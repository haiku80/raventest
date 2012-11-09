using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using StackExchange.Profiling;
using StackExchange.Profiling.MVCHelpers;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RavenTest.App_Start.MiniProfilerPackage), "PreStart")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(RavenTest.App_Start.MiniProfilerPackage), "PostStart")]
namespace RavenTest.App_Start
{
  public static class MiniProfilerPackage
  {
    public static void PreStart()
    {
      //Make sure the MiniProfiler handles BeginRequest and EndRequest
      DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));

      //Setup profiler for Controllers via a Global ActionFilter
      GlobalFilters.Filters.Add(new ProfilingActionFilter());
    }

    public static void PostStart()
    {
      // Intercept ViewEngines to profile all partial views and regular views.
      // If you prefer to insert your profiling blocks manually you can comment this out
      var copy = ViewEngines.Engines.ToList();
      ViewEngines.Engines.Clear();
      foreach (var item in copy)
      {
        ViewEngines.Engines.Add(new ProfilingViewEngine(item));
      }
    }
  }

  public class MiniProfilerStartupModule : IHttpModule
  {
    public void Init(HttpApplication context)
    {
      context.BeginRequest += (sender, e) =>
      {
        var request = ((HttpApplication)sender).Request;
        if (request.IsLocal) { MiniProfiler.Start(); }
      };

      context.EndRequest += (sender, e) =>
      {
        MiniProfiler.Stop();
      };
    }

    public void Dispose() { }
  }
}


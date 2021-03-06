﻿using MvcMiniProfiler.RavenDb;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace RavenTest
{
  public class RavenDBNinjectModule : NinjectModule
  {
    public override void Load()
    {
      Bind<IDocumentStore>()
        .ToMethod(context =>
          {
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
            var documentStore = new EmbeddableDocumentStore { DataDirectory = "App_Data", UseEmbeddedHttpServer = true, };
            var iDocumentStore = documentStore.Initialize();
            Profiler.AttachTo(documentStore);
            return iDocumentStore;
          })
        .InSingletonScope();

      Bind<IDocumentSession>()
        .ToMethod(context => context.Kernel.Get<IDocumentStore>().OpenSession())
        .InRequestScope();
    }
  }
}
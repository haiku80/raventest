using Raven.Client;
using RavenTest.Controllers.Base;
using System.Web.Mvc;

namespace RavenTest.Controllers
{
  public class HomeController : BaseController
  {
    public HomeController(IDocumentSession documentSession) : base(documentSession) { }

    public ActionResult Index()
    {
      return View();
    }
  }
}

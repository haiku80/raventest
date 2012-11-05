using Raven.Client;
using RavenTest.Controllers.Base;
using RavenTest.Models;
using System.Web.Mvc;

namespace RavenTest.Controllers
{
  public class HomeController : BaseController
  {
    public HomeController(IDocumentSession documentSession) : base(documentSession) { }

    public ActionResult Index()
    {
      var users = RavenSession.Query<User>();

      return View(users);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(User user)
    {
      RavenSession.Store(user);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Edit(string id)
    {
      var user = RavenSession.Load<User>(id);

      return View(user);
    }

    [HttpPost]
    public ActionResult Edit(string id, User user)
    {
      RavenSession.Store(user);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete(string id)
    {
      var user = RavenSession.Load<User>(id);

      return View(user);
    }

    [HttpPost]
    public ActionResult Delete(string id, string confirmDelete)
    {
      var user = RavenSession.Load<User>(id);

      RavenSession.Delete(user);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }
  }
}

using Raven.Client;
using RavenTest.Controllers.Base;
using RavenTest.Models;
using System.Web.Mvc;

namespace RavenTest.Controllers
{
  public class SectionController : BaseController
  {
    public SectionController(IDocumentSession documentSession) : base(documentSession) { }

    public ActionResult Index()
    {
      var sections = RavenSession.Query<Section>();

      return View(sections);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Section section)
    {
      RavenSession.Store(section);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Edit(string id)
    {
      var section = RavenSession.Load<Section>(id);

      return View(section);
    }

    [HttpPost]
    public ActionResult Edit(string id, Section section)
    {
      RavenSession.Store(section);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete(string id)
    {
      var section = RavenSession.Load<Section>(id);

      return View(section);
    }

    [HttpPost]
    public ActionResult Delete(string id, string confirmDelete)
    {
      var section = RavenSession.Load<Section>(id);

      RavenSession.Delete(section);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }
  }
}

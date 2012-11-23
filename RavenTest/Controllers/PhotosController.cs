using Raven.Client;
using RavenTest.Controllers.Base;
using RavenTest.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace RavenTest.Controllers
{
  public class PhotosController : BaseController
  {
    public PhotosController(IDocumentSession documentSession) : base(documentSession) { }

    public ActionResult Create(string id)
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(string id, HttpPostedFile file)
    {
      var blogPost = RavenSession.Load<BlogPost>(id);

      var photo = new Photo { Path = "/public/" + Guid.NewGuid() };
      file.SaveAs(Server.MapPath(photo.Path));
      blogPost.Photos.Add(photo);
  
      RavenSession.SaveChanges();

      return RedirectToRoute(new { controller = "Blog", action = "Edit", id = id });
    }

    public ActionResult Delete(string id)
    {
      var photo = RavenSession.Load<Photo>(id);

      return View(photo);
    }

    [HttpPost]
    public ActionResult Delete(string id, string confirmDelete)
    {
      var photo = RavenSession.Load<Photo>(id);

      RavenSession.Delete(photo);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }
  }
}

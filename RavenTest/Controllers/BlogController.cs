using Raven.Client;
using RavenTest.Controllers.Base;
using RavenTest.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RavenTest.Controllers
{
  public class BlogController : BaseController
  {
    public BlogController(IDocumentSession documentSession) : base(documentSession) { }

    public ActionResult Index()
    {
      var blogPosts = RavenSession.Query<BlogPost>().OrderByDescending(b => b.Created);

      return View(blogPosts);
    }

    public ActionResult Create()
    {
      return View(new BlogPost());
    }

    [HttpPost]
    public ActionResult Create(BlogPost blogPost)
    {
      RavenSession.Store(blogPost);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Edit(string id)
    {
      var blogPost = RavenSession.Load<BlogPost>(id);

      return View(blogPost);
    }

    [HttpPost]
    public ActionResult Edit(string id, BlogPost blogPost)
    {
      blogPost.LastModified = DateTime.Now;
      RavenSession.Store(blogPost);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Delete(string id)
    {
      var blogPost = RavenSession.Load<BlogPost>(id);

      return View(blogPost);
    }

    [HttpPost]
    public ActionResult Delete(string id, string confirmDelete)
    {
      var blogPost = RavenSession.Load<BlogPost>(id);

      RavenSession.Delete(blogPost);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }
  }
}

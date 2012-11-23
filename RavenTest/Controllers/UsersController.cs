using AutoMapper;
using Raven.Client;
using RavenTest.Controllers.Base;
using RavenTest.Models;
using RavenTest.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RavenTest.Controllers
{
  public class UsersController : BaseController
  {
    public UsersController(IDocumentSession documentSession) : base(documentSession) { }

    public ActionResult Index()
    {
      var users = RavenSession.Query<User>();

      return View(users);
    }

    public ActionResult Create()
    {
      return View(CreateUserViewModel());
    }

    [HttpPost]
    public ActionResult Create(UserViewModel userViewModel)
    {
      userViewModel.User.ActiveSections = Mapper.Map<IEnumerable<SectionViewModel>, List<Section>>(userViewModel.SiteSections.Where(ss => ss.IsActive));
      RavenSession.Store(userViewModel.User);
      RavenSession.SaveChanges();

      return RedirectToAction("Index");
    }

    public ActionResult Edit(string id)
    {
      var user = RavenSession.Load<User>(id);

      return View(CreateUserViewModel(user));
    }

    [HttpPost]
    public ActionResult Edit(string id, UserViewModel userViewModel)
    {
      userViewModel.User.LastModified = DateTime.Now;
      userViewModel.User.ActiveSections = Mapper.Map<IEnumerable<SectionViewModel>, List<Section>>(userViewModel.SiteSections.Where(ss => ss.IsActive));
      RavenSession.Store(userViewModel.User);
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

    private UserViewModel CreateUserViewModel()
    {
      return CreateUserViewModel(new User());
    }

    private UserViewModel CreateUserViewModel(User user)
    {
      var activeSectionIDs = user.ActiveSections.Select(s => s.Id).ToList();

      return new UserViewModel
      {
        User = user,
        SiteSections = RavenSession
                        .Query<Section>()
                        .ToList()
                        .Select(s => new SectionViewModel
                        {
                          Id = s.Id,
                          Name = s.Name,
                          Url = s.Url,
                          IsActive = activeSectionIDs.Contains(s.Id)
                        })
                        .ToList()
                        
      };
    }
  }
}

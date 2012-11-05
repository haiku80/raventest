using Raven.Client;
using System.Web.Mvc;

namespace RavenTest.Controllers.Base
{
  public abstract class BaseController : Controller
  {
    private readonly IDocumentSession _documentSession;
    public IDocumentSession RavenSession 
    {
      get
      {
        return _documentSession;
      }
    }

    public BaseController(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }
  }
}

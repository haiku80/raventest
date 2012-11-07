using System.Collections.Generic;

namespace RavenTest.Models.ViewModels
{
  public class UserViewModel
  {
    public User User { get; set; }
    public List<SectionViewModel> SiteSections { get; set; }
  }
}
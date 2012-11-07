using AutoMapper;
using RavenTest.Models;
using RavenTest.Models.ViewModels;

namespace RavenTest.App_Start
{
  public static class AutoMapperWebConfiguration
  {
    public static void Configure()
    {
      Mapper.Initialize(cfg =>
      {
        cfg.AddProfile(new UtenteProfile());
      });
    }
  }

  public class UtenteProfile : Profile
  {
    protected override void Configure()
    {
      Mapper.CreateMap<SectionViewModel, Section>();
    }
  }
}
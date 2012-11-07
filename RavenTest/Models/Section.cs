using System.ComponentModel.DataAnnotations;

namespace RavenTest.Models
{
  public class Section
  {
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Url { get; set; }
  }
}
using System.ComponentModel.DataAnnotations;

namespace RavenTest.Models
{
  public class Photo
  {
    [Required]
    [StringLength(250)]
    public string Path { get; set; }
  }
}
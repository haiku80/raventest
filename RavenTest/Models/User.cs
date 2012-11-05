using System;
using System.ComponentModel.DataAnnotations;

namespace RavenTest.Models
{
  public class User
  {
    [Required]
    public string Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [StringLength(15)]
    public string IP { get; set; }

    public DateTime? DataUltimaModifica { get; set; }
  }
}
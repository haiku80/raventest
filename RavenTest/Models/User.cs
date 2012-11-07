using System;
using System.Collections.Generic;
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
    [Display(Name = "Last Modified")]
    public DateTime? LastModified { get; set; }

    public List<Section> ActiveSections { get; set; }

    public User()
    {
      ActiveSections = new List<Section>();
    }
  }
}
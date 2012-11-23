using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RavenTest.Models
{
  public class BlogPost
  {
    [Required]
    public string Id { get; set; }
    [Required]
    public string Title { get; set; }
    [StringLength(30)]
    public string Subtitle { get; set; }
    [Required]
    [AllowHtml]
    [DataType(DataType.MultilineText)]
    public string Body { get; set; }
    [Required]
    [Display(Name = "Created on")]
    public DateTime Created { get; set; }
    [Display(Name = "Last Modified")]
    public DateTime? LastModified { get; set; }

    public List<Photo> Photos { get; set; }

    public BlogPost()
    {
      Created = DateTime.Now;
      Photos = new List<Photo>();
    }
  }
}
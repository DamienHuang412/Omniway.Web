using System.ComponentModel.DataAnnotations;

namespace Practice.Web.Mvc.Models;

public class RegisterViewModel
{
    [Display(Name = "User Name")]
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}
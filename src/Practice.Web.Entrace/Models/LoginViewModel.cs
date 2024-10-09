using System.ComponentModel.DataAnnotations;

namespace Practice.Web.App.Models;

public class LoginViewModel
{
    [Display(Name = "User Name")]
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string UserName { get; set; }

    [Display(Name = "Password")]
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Omniway.Web.App.Models;

public class LoginViewModel
{
    [Display(Name = "User Name")]
    [Required]
    [MinLength(1)]
    [MaxLength(20)]
    public string UserName { get; set; }

    [Display(Name = "Password")]
    [Required]
    [MinLength(1)]
    [MaxLength(20)]
    public string Password { get; set; }
}
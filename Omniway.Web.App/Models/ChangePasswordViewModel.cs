using System.ComponentModel.DataAnnotations;

namespace Omniway.Web.App.Models;

public class ChangePasswordViewModel
{
    [Display(Name = "Current password")]
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string OldPassword { get; set; }

    [Display(Name = "New password")]
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string NewPassword { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Omniway.Web.App.Models;

public class ChangePasswordViewModel
{
    [Display(Name = "Current password")]
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string OldPassword { get; set; }

    [Display(Name = "New password")]
    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string NewPassword { get; set; }
}
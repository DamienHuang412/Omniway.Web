using System.ComponentModel.DataAnnotations;

namespace Practice.Web.Api.DTOs;

public class ChangePasswordDTO
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
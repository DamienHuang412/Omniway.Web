using System.ComponentModel.DataAnnotations;

namespace Omniway.Web.App.DTOs;

public class ChangePasswordDTO
{
    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string OldPassword { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string NewPassword { get; set; }
}
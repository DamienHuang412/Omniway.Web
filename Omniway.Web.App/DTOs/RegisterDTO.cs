using System.ComponentModel.DataAnnotations;

namespace Omniway.Web.App.DTOs;

public class RegisterDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(20)]
    public string UserName { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string Password { get; set; }
}
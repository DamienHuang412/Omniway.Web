using System.ComponentModel.DataAnnotations;

namespace Practice.Web.BlazorApp.Models;

public class UserFormModel
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string UserName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    public string Password { get; set; }
}
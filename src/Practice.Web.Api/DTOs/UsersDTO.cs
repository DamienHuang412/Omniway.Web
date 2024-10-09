using System.ComponentModel.DataAnnotations;

namespace Practice.Web.Api.DTOs;

public class UsersDTO
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    
    public int PageSize { get; set; } = 5;
}
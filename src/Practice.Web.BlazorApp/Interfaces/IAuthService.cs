using Practice.Web.BlazorApp.Models;

namespace Practice.Web.BlazorApp.Interfaces;

public interface IAuthService
{
    Task<bool> Login(UserFormModel request);
    
    Task Logout();
}
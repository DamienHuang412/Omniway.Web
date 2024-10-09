using Practice.Web.BlazorApp.Models;

namespace Practice.Web.BlazorApp.Interfaces;

public interface IUserService
{
    Task<bool> Register(UserFormModel request);
    
    Task<DashboardModel<UserModel>> GetUsers(int page = 1, int pageSize = 5);
}
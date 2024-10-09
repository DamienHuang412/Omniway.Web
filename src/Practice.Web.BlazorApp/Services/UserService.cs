using System.Net.Http.Json;
using Practice.Web.BlazorApp.Interfaces;
using Practice.Web.BlazorApp.Models;

namespace Practice.Web.BlazorApp.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> Register(UserFormModel request)
    {
        var response = await _httpClient.PostAsJsonAsync("/register", request);

        return response.IsSuccessStatusCode;
    }

    public async Task<DashboardModel<UserModel>> GetUsers(int page = 1, int pageSize = 5)
    {
        var response = await _httpClient.GetAsync($"/users?page={page}&pageSize={pageSize}");

        if (!response.IsSuccessStatusCode)
        {
            return new DashboardModel<UserModel>();
        }
        
        return await response.Content.ReadFromJsonAsync<DashboardModel<UserModel>>();
    }
}
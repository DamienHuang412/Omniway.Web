using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Practice.Web.BlazorApp.Constants;
using Practice.Web.BlazorApp.Interfaces;
using Practice.Web.BlazorApp.Models;
using Practice.Web.BlazorApp.Utilities;

namespace Practice.Web.BlazorApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    public async Task<bool> Login(UserFormModel request)
    {
        var result = false;

        var response = await _httpClient.PostAsJsonAsync("/login", request);

        if (!response.IsSuccessStatusCode) return result;
        
        var resContent = await response.Content.ReadAsStringAsync();
        var tokenResult = JsonSerializer.Deserialize<AuthenticationModel>(resContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (tokenResult == null || string.IsNullOrEmpty(tokenResult.Token)) return result;

        await _localStorageService.SetItemAsStringAsync(HardCode.LocalStorage.AuthToken, tokenResult.Token);

        ((JwtAuthenticationStateProvider)_authenticationStateProvider).NotifyUserAuthentication(tokenResult.Token);

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResult.Token);

        result = true;

        return result;
    }

    public async Task Logout()
    {
        await _localStorageService.RemoveItemAsync(HardCode.LocalStorage.AuthToken);
        ((JwtAuthenticationStateProvider)_authenticationStateProvider).NotifyUserLogOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
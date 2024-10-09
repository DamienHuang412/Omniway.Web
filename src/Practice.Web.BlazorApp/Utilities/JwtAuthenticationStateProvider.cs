using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Practice.Web.BlazorApp.Constants;

namespace Practice.Web.BlazorApp.Utilities;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;        
        
    private readonly AuthenticationState _anonymous;

    public JwtAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    { 
        var tokenInLocalStorage = await _localStorageService.GetItemAsStringAsync(HardCode.LocalStorage.AuthToken);
        if (string.IsNullOrEmpty(tokenInLocalStorage))
        {
            return _anonymous;
        }
        var claims = JwtHelper.ParseClaimsFromJwt(tokenInLocalStorage);

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenInLocalStorage);

        //回傳帶有user claim的AuthenticationState物件
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
    }
        
    public void NotifyUserAuthentication(string token)
    {
        var claims = JwtHelper.ParseClaimsFromJwt(token);
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogOut()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
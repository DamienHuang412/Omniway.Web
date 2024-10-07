using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.DTOs;
using Omniway.Web.Core.Interfaces;

namespace Omniway.Web.App.ApiEndpoints;

[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private const string UserName = "UserName";
    
    public AuthenticateController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody]LoginDTO request)
    {
        var result = await _authenticationService.Login(request.UserName, request.Password, HttpContext.RequestAborted);

        if (!result.IsSuccess) return BadRequest();
        
        HttpContext.Response.Cookies.Append(UserName, request.UserName, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddHours(1)});
            
        return Ok(new { result.Token });
    }

    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Response.Cookies.Delete(UserName, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddHours(1)});

        return Ok();
    }
}
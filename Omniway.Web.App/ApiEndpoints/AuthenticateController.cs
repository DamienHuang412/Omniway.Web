using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Omniway.Web.App.Constants;
using Omniway.Web.App.Models;
using Omniway.Web.Core.Interfaces;
using System.Text.Encodings;
using System.Text.Json;
using Omniway.Web.App.Interfaces;
using Omniway.Web.App.Managers;

namespace Omniway.Web.App.ApiEndpoints;

[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAllowlistManager _allowlistManager;
    
    public AuthenticateController(IAuthenticationService authenticationService, IAllowlistManager allowlistManager)
    {
        _authenticationService = authenticationService;
        _allowlistManager = allowlistManager;
    }

    [AllowAnonymous]
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody]LoginViewModel request)
    {
        var result = await _authenticationService.Login(request.UserName, request.Password, HttpContext.RequestAborted);

        if (!result.IsSuccess) return BadRequest();

        _allowlistManager.AddAllowlist(request.UserName);
            
        return Ok(new { result.Token.Token });
    }

    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated &&
            !string.IsNullOrEmpty(HttpContext.User.Identity.Name))
        {
            _allowlistManager.RemoveAllowlist(HttpContext.User.Identity.Name);
        }
        
        return Ok();
    }
}
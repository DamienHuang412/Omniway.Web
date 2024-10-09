using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Web.Api.DTOs;
using Practice.Web.Api.Interfaces;
using Practice.Web.Core.Interfaces;

namespace Practice.Web.Api.Controllers;

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
    public async Task<IActionResult> Login([FromBody]LoginDTO request)
    {
        if (!ModelState.IsValid) return BadRequest();
        
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
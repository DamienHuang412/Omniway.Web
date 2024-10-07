using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.DTOs;
using Omniway.Web.Core.Interfaces;
using Omniway.Web.Core.Models;

namespace Omniway.Web.App.ApiEndpoints;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO request)
    {
        _ = await _userService.Create(new RegisterModel
        {
            UserName = request.UserName,
            RawPassword = request.Password
        }, HttpContext.RequestAborted);

        return Created();
    }

    [Authorize]
    [HttpPost("/change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO request)
    {
        try
        {
            await _userService.ChangePassword(new ChangePasswordModel
            {
                UserName = User.Identity.Name,
                OldPassword = request.OldPassword,
                NewPassword = request.NewPassword
            }, HttpContext.RequestAborted);
        
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
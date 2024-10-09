using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Web.App.Attributes;
using Practice.Web.App.Models;
using Practice.Web.Core.Interfaces;
using Practice.Web.Core.Models;

namespace Practice.Web.App.ApiControllers;

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
    public async Task<IActionResult> Register([FromBody] RegisterViewModel request)
    {
        _ = await _userService.Create(new RegisterModel
        {
            UserName = request.UserName,
            RawPassword = request.Password
        }, HttpContext.RequestAborted);

        return Created();
    }

    [Authorize]
    [AllowlistAuthorize]
    [HttpPost("/change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel request)
    {
        if (!ModelState.IsValid) return BadRequest();
        
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
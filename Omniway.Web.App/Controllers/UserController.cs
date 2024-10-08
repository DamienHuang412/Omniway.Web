using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Attributes;
using Omniway.Web.App.Interfaces;
using Omniway.Web.App.Models;
using Omniway.Web.Core.Interfaces;
using Omniway.Web.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Omniway.Web.App.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [AllowlistAuthorize]
    [SwaggerIgnore]
    public IActionResult Index()
    {
        return View(new UserViewModel
        {
            UserName = HttpContext.User.Identity.Name
        });
    }

    [HttpGet]
    [SwaggerIgnore]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [SwaggerIgnore]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
    {
        _ = await _userService.Create(new RegisterModel
        {
            UserName = model.UserName,
            RawPassword = model.Password
        }, HttpContext.RequestAborted);
        
        return RedirectToAction("Login", "Auth");
    }
}
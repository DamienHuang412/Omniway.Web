using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Attributes;
using Omniway.Web.App.Models;
using Practice.Web.Core.Interfaces;
using Practice.Web.Core.Models;
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
    public IActionResult RegisterUser()
    {
        return View();
    }

    [HttpPost]
    [SwaggerIgnore]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterViewModel model)
    {
        if(!ModelState.IsValid)
            return View();
        
        _ = await _userService.Create(new RegisterModel
        {
            UserName = model.UserName,
            RawPassword = model.Password
        }, HttpContext.RequestAborted);
        
        return RedirectToAction("Login", "Auth");
    }
}
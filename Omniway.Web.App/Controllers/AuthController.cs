using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Constants;
using Omniway.Web.App.Models;

namespace Omniway.Web.App.Controllers;

public class AuthController : Controller
{
    private readonly Core.Interfaces.IAuthenticationService _authenticationService;

    public AuthController(Core.Interfaces.IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View();
        var result = _authenticationService.Login(model.UserName, model.Password, HttpContext.RequestAborted)
            .GetAwaiter().GetResult();

        if (!result.IsSuccess) return View();

        HttpContext.User = new ClaimsPrincipal(result.Token.ClaimsIdentity);

        return View("../User/Index", new UserViewModel
        {
            UserName = result.Token.ClaimsIdentity.FindFirst("sub").Value
        });
    }
    
    public IActionResult Logout()
    {
        return RedirectToAction("Index", "Home");
    }

}
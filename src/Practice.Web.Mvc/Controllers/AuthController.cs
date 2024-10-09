using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Web.Mvc.Constants;
using Practice.Web.Mvc.Models;

namespace Practice.Web.Mvc.Controllers;

public class AuthController : Controller
{
    private readonly Practice.Web.Core.Interfaces.IAuthenticationService _authenticationService;

    public AuthController(Practice.Web.Core.Interfaces.IAuthenticationService authenticationService)
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
        
        HttpContext.Response.Cookies.Append(HardCode.Cookie.JwtToken, result.Token.Token, new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(10)
        });

        return RedirectToAction("Index", "User");
    }
    
    public IActionResult Logout()
    {
        Response.Cookies.Delete(HardCode.Cookie.JwtToken);
        
        return RedirectToAction("Index", "Home");
    }

}
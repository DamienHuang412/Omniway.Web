using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Models;

namespace Omniway.Web.App.Controllers;

[Authorize]
public class UserController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.User.Identity is not null)
        {
            return View(new UserViewModel
            {
                UserName = HttpContext.User.Identity.Name ?? HttpContext.User.Claims.First(c => c.Type == "sub").Value,
            });
        }
        
        return RedirectToAction("Index", "Home");
    }
}
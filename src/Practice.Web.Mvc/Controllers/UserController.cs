using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice.Web.Core.Interfaces;
using Practice.Web.Core.Models;
using Practice.Web.Mvc.Models;

namespace Practice.Web.Mvc.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var data = await _userService.Read(page, pageSize, HttpContext.RequestAborted);

        return View(new UsersViewModel
        {
            Page = page,
            PageSize = pageSize,
            TotalCount = data.TotalCount,
            TotalPage = (int)Math.Ceiling(data.TotalCount / (double)pageSize),
            Data = data.Data
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    UserName = x.UserName
                }).ToArray()
        });
    }

    [HttpGet]
    public IActionResult RegisterUser()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
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
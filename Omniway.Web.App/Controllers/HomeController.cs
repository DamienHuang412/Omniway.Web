using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Constants;
using Omniway.Web.App.Models;
using IAuthenticationService = Omniway.Web.Core.Interfaces.IAuthenticationService;

namespace Omniway.Web.App.Controllers;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
    private readonly ILogger _logger;
    private readonly IAuthenticationService _authenticationService;

    public HomeController(ILogger<HomeController> logger,
        IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }
    
    [Route("")]
    [Route("/")]
    [Route("/Home")]
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
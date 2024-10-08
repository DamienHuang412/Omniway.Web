using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Models;
using Swashbuckle.AspNetCore.Annotations;
using IAuthenticationService = Omniway.Web.Core.Interfaces.IAuthenticationService;

namespace Omniway.Web.App.Controllers;

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
    
    [SwaggerIgnore]
    [Route("")]
    [Route("/")]
    [Route("/Home")]
    public IActionResult Index()
    {
        return View();
    }
    
    [SwaggerIgnore]
    public IActionResult Privacy()
    {
        return View();
    }

    [SwaggerIgnore]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
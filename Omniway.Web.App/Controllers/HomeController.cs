using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Omniway.Web.App.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Omniway.Web.App.Controllers;

public class HomeController : Controller
{
    [SwaggerIgnore]
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
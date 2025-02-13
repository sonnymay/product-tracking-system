using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySimpleWebApp.Models;
using System.Threading.Tasks;

namespace MySimpleWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Index page loading at {Time}", DateTime.UtcNow);
        await Task.CompletedTask;
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        _logger.LogInformation("Privacy page loading at {Time}", DateTime.UtcNow);
        await Task.CompletedTask;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("An error occurred. RequestId: {RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

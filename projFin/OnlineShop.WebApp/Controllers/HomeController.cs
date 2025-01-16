using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.WebApp.Models;

namespace OnlineShop.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Warunki u¿ytkowania
    public IActionResult Terms()
    {
        return View();
    }

    // O nas
    public IActionResult AboutUs()
    {
        return View();
    }

    // Kontakt
    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

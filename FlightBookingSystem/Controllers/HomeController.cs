using System.Diagnostics;
using FlightBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.Controllers;



public class HomeController : Controller
{
    LocationModel model = null;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    

    public IActionResult Index()
    {
        if (model == null) {
            model = new LocationModel();
        } 
        model.Country = "kosove";
        model.City = "Prishtine";
        return View(model);
    }

    public IActionResult Privacy()
    {
        LocationModel model = new LocationModel();
        model.Country = "kosove";
        model.City = "Prishtine";
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


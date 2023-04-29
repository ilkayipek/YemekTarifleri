using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Models;

namespace YemekTarifleri.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.title = "Home Page";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("DetailRecipe.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Models;
using YemekTarifleri.DataBase;
using Microsoft.EntityFrameworkCore;

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
        ApplicationConnectionDb db = new ApplicationConnectionDb();
        ViewBag.Recipes = db.recips.ToList();
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


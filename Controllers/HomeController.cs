using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Models;
using YemekTarifleri.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace YemekTarifleri.Controllers;

[AllowAnonymous]
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

        List<Recipe> recipesWithUsers = db.recipes
    .Include(r => r.user) // Kullanıcı bilgilerini ekleyin
    .ToList();

        ViewBag.RecipesWithUsers = recipesWithUsers;

        Recipe? mealOfTheDayRecipe = db.recipes
    .FirstOrDefault(r => r.isMealOfDay == true);


        ViewBag.IsMealOfDay = mealOfTheDayRecipe;

        //HttpContext.Request.Cookies.TryGetValue("userId",out string? userId);
        string? userId = HttpContext.Request.Cookies["userId"];
        _logger.LogInformation($"userId: {userId}");

       if (userId != null) {
                User? user = db.users.FirstOrDefault(u => (u.id == int.Parse(userId)));
            UserInfo.setUser(user);
        }

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


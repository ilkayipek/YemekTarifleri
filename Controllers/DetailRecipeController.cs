using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.DataBase;
using YemekTarifleri.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    public class DetailRecipeController : Controller
    {
        // GET: /<controller>/
        public IActionResult DetailRecipe(int detailId)
        {
            ApplicationConnectionDb db = new ApplicationConnectionDb();

            ViewBag.DetailId = detailId;
            var recipeWithUser = db.recipes
    .Include(r => r.user) // Kullanıcı bilgilerini ekleyin
    .FirstOrDefault(r => r.id == detailId); // Tarif Id'sine göre filtreleyin

            ViewBag.RecipeWithUser = recipeWithUser;
            return View();
        }
    }
}


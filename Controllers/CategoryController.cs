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
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        public IActionResult CategoryList(int categoryId, string categoryName)
        {
            ViewBag.categoryId = categoryId;
            ApplicationConnectionDb db = new ApplicationConnectionDb();

            List<Recipe> recipeWithUsers = db.recipes
                .Include(r => r.user)
                .Where(r => r.categoryId == categoryId).ToList();

            ViewBag.RecipeWithUsers = recipeWithUsers;
            ViewBag.CategoryName = categoryName;

            return View();
        }


    }
}


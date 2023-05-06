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
    public class SearchController : Controller
    {
        // GET: /<controller>/
        [HttpPost]
        public IActionResult Search(string searchInput)
        {
            ApplicationConnectionDb db = new ApplicationConnectionDb();
            List<Recipe> searchResults = db.recipes
                .Include(r => r.user) // Kullanıcı bilgilerini ekleyin
                .Where(r => r.name.Contains(searchInput) || r.ingredients.Contains(searchInput))
                .ToList();

            ViewBag.SearchResults = searchResults;

            return View();
        }
    }
}


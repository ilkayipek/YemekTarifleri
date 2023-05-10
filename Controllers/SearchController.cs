using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.DataBase;
using YemekTarifleri.Models;



namespace YemekTarifleri.Controllers
{
    public class SearchController : Controller
    {
        
        [HttpPost]
        public IActionResult Search(string searchInput)
        {
            ApplicationConnectionDb db = new ApplicationConnectionDb();
            var searchResult = db.recipes
    .Include(r => r.category)
    .Include(r => r.user)
    .Where(r => r.name.Contains(searchInput) || r.category.name.Contains(searchInput))
    .ToList();

            ViewBag.SearchResults = searchResult;

            return View();
        }
    }
}


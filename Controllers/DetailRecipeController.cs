using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.DataBase;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    public class DetailRecipeController : Controller
    {
        // GET: /<controller>/
        public IActionResult DetailRecipe(int detailId)
        {
            ViewBag.DetailId = detailId;
            using (var context = new ApplicationConnectionDb())
            {
                var recipe = context.recipes.FirstOrDefault(x => x.id == detailId);
                ViewBag.DetailRecipe = recipe;
            }
            
            return View();
        }
    }
}


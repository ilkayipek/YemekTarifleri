using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.DataBase;
using YemekTarifleri.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    public class CategoryController : Controller
    {
        // GET: /<controller>/
        public IActionResult CategoryList(string categoryId)
        {
            ViewBag.categoryId = categoryId;
            return View();
        }


    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YemekTarifleri.DataBase;
using YemekTarifleri.Models;

namespace YemekTarifleri.ViewComponents
{
    public class CategoryLayout : ViewComponent
    {
        private readonly ApplicationConnectionDb _context;

        public CategoryLayout(ApplicationConnectionDb context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.categories.ToListAsync();
            return View(categories);
        }
    }
}

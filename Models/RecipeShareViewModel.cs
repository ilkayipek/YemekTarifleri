using System;
using Microsoft.AspNetCore.Http;

namespace YemekTarifleri.Models
{
    public class RecipeShareViewModel
    {
        public string? name { get; set; }
        public int categoryId { get; set; }
        public IFormFile? image { get; set; }
        public string? ingredients { get; set; }
        public string? cooking { get; set; }
    }
}

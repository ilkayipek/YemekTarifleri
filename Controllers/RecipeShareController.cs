using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Models;
using YemekTarifleri.DataBase;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    public class RecipeShareController : Controller
    {

        ApplicationConnectionDb db = new ApplicationConnectionDb();



        public IActionResult RecipeShare()
        {
            List<Category> categories = db.categories.ToList();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecipeShare(RecipeShareViewModel model)
        {
            User? currentUser = UserInfo.getUser();
            List<Category> categories = db.categories.ToList();
            ViewBag.Categories = categories;

            if (ModelState.IsValid)
            {
                if (model.image != null && currentUser != null && await UploadImage(model.image, currentUser!.userName))
                {
                    // Yüklenen resim var ve başarıyla yüklendiğinde devam edin
                    Recipe newRecipe = new Recipe()
                    {
                        name = model.name,
                        ingredients = model.ingredients,
                        UserId = currentUser?.id,
                        date = DateTime.Now.ToString(),
                        cooking = model.cooking,
                        categoryId = model.categoryId,
                        isMealOfDay = false,
                        image = $"/UserImages/{model.image.FileName}"
                        
                    };
                    db.recipes.Add(newRecipe);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Fotoğraf yükleme sırasında bir hata oluştu.");
                }
            }
            else
            {

                ModelState.AddModelError("", "Tarif yükleme işlemi başarısız.");
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                // Hata mesajlarını loglama veya hata ayıklama amacıyla kullanabilirsiniz
                foreach (var error in errors)
                {
                    // Hata mesajlarını kullanabilirsiniz
                    var errorMessage = error.ErrorMessage;
                }
            }

            return View(model);
        }

        [HttpPost("upload")]
        public async Task<bool> UploadImage(IFormFile file, string userName)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            try
            {
                string targetFolderPath = @"/Users/macbook/Documents/GitHub/YemekTarifleri/YemekTarifleri/wwwroot/UserImages/";
                var imagePath = Path.Combine(targetFolderPath, file.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Hata: " + ex.ToString();
                return false;
            }

        }
    }
}

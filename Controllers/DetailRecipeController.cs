using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.DataBase;
using YemekTarifleri.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    [AllowAnonymous]
    public class DetailRecipeController : Controller
    {

        ApplicationConnectionDb db = new ApplicationConnectionDb();

        // GET: /<controller>/
        public IActionResult DetailRecipe(int detailId)
        {
            ViewBag.DetailId = detailId;

            var recipeWithUser = db.recipes
    .Include(r => r.user) // Kullanıcı bilgilerini ekleyin
    .FirstOrDefault(r => r.id == detailId); // Tarif Id'sine göre filtreleyin

            var commentsWithUsers = getComments(detailId);
            ViewBag.CommentsWithUsers = commentsWithUsers;
            ViewBag.RecipeWithUser = recipeWithUser;
            return View();
        }

        private List<Comment>? getComments(int recipeId)
        {
            List<Comment>? commentsWithUsers = db.comments
                .Where(c => c.recipeId == recipeId)
                .Include(c => c.user)
                .ToList();

            return commentsWithUsers;
        }

        [HttpPost]
        [Route("/DetailRecipe/postComment")]
        public IActionResult postComment(string? contents,int recipeId, double point)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı ve tarifi alın
                User? currentUser = UserInfo.getUser();

                // Yorum nesnesini oluşturun ve verileri doldurun
                Comment newComment = new Comment
                {
                    date = DateTime.Now.ToString(),
                    approvalStatus = true,
                    contents = contents,
                    recipeId = recipeId,
                    userId = currentUser?.id,
                    point = point
                };

                // Yorumu veritabanına kaydedin
                db.comments.Add(newComment);
                db.SaveChanges();

                return RedirectToAction("DetailRecipe", "DetailRecipe", new { detailId = recipeId, focusBottom = true });
            }
            else {
                ModelState.AddModelError("", "Yorum paylaşma işlemi başarısız.");
              
            }

            return View();
        }
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YemekTarifleri.DataBase;
using YemekTarifleri.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    [AllowAnonymous]
    public class SignInController : Controller
    {

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel model,bool rememberMe)
        {
            model.IsSubmitted = true;

            if (ModelState.IsValid)
            {
                // Model doğrulama başarılı ise, giriş işlemlerini gerçekleştirin
                if (IsValidUser(model.emailOrUsername, model.password))
                {
                   var userId = getUserId(model.emailOrUsername);

                    if (userId != null) {
                        var userid = userId.ToString();
                        if (rememberMe)
                        {
                            DateTime expires = DateTime.UtcNow.AddDays(7);
                            HttpContext.Response.Cookies.Append("userId", $"{userid}",  new CookieOptions { Expires = expires });
                        }
                        else
                        {
                            DateTime expires = DateTime.UtcNow.AddHours(7);
                            HttpContext.Response.Cookies.Append("userId", $"{userid}", new CookieOptions { Expires = expires });
                        }
                        // Giriş başarılı ise, kullanıcıyı yönlendirin
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTimeOffset.MaxValue
                        };
                        

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    // Giriş başarısız ise, hata mesajını ModelState'e ekle
                    ModelState.AddModelError("", "Geçersiz email, kullanıcı adı veya şifre");
                }
            }

            // Model doğrulama başarısız ise, SignIn.cshtml sayfasını tekrar yükle
            return View(model);
        }

        private int? getUserId(string emailOrUsername) {
            ApplicationConnectionDb db = new ApplicationConnectionDb();
            var user = db.users.FirstOrDefault(k => (k.userName == emailOrUsername || k.email == emailOrUsername));
            return user?.id;
        }

        private bool IsValidUser(string emailAndUserName, string password)
        {
            ApplicationConnectionDb db = new ApplicationConnectionDb();

            var user = db.users.FirstOrDefault(u => (u.email == emailAndUserName || u.userName == emailAndUserName) && u.password == password);
            if (user != null)
            {
                // Kullanıcı doğrulandı
                // Kullanıcının oturum kimliğini ve diğer bilgileri güncelleyebilirsiniz
                return true;
            }

            return false;
        }

    }
}


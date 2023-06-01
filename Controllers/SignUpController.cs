using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemekTarifleri.Models;
using static System.Net.WebRequestMethods;
using YemekTarifleri.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;


namespace YemekTarifleri.Controllers
{
    
    public class SignUpController : Controller
    {

        private readonly IServiceProvider _serviceProvider;

        public SignUpController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        ApplicationConnectionDb db = new ApplicationConnectionDb();


        public IActionResult SignUp() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel model, bool provisions)
        {
            // Kullanıcı kaydı işlemleri...
            if (ModelState.IsValid)
            {
                // Model doğrulama başarılı ise, giriş işlemlerini gerçekleştirin
                if (confirmPassword(model.password, model.againPassword))
                {
                    if (provisions)
                    {
                        if (!containsEmail(model.email))
                        {
                            if (!containsUserName(model.userName))
                            {
                                if (IsPasswordValid(model.password))
                                {
                                    User newUser = new User()
                                    {
                                        nameAndSurname = model.nameAndSurname,
                                        userName = model.userName,
                                        password = model.password,
                                        email = model.email,
                                        avatar = generateAvatar(model.gender),
                                        gender = model.gender
                                    };

                                    CreateUserFolder(model.userName);

                                    db.users.Add(newUser);
                                    db.SaveChanges();

                                    

                                    return RedirectToAction("SignIn", "SignIn");
                                }
                                else {
                                    ModelState.AddModelError("", "Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermeli");
                                }
                                
                            }
                            else {
                                ModelState.AddModelError("", "Girilen kullanıcı adı Başka bir hesap Tarafından kullanılıyor.");
                            }
                            
                        }
                        else {
                            ModelState.AddModelError("","Girilen email Başka bir hesap Tarafından kullanılıyor.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kayıt olmak için hüküm ve koşulları kabul etmeniz gerekli.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Şifreler uyuşmuyor");
                }
            }
            else {
                ModelState.AddModelError("", "Kayıt işlemi Başarısız oldu.");
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

        private bool confirmPassword(string password, string againPassword) {
            if (password == againPassword) {
                return true;
            }
            return false;
        }

       

        private string generateAvatar(bool gender) {
            Random random = new Random();
            int randomInRange = random.Next(0, 5);
            string[] menPath = { "https://e7.pngegg.com/pngimages/799/987/png-clipart-computer-icons-avatar-icon-design-avatar-heroes-computer-wallpaper-thumbnail.png",
                "https://e7.pngegg.com/pngimages/168/549/png-clipart-computer-icons-avatar-male-super-b-face-heroes-thumbnail.png",
                "https://e7.pngegg.com/pngimages/7/618/png-clipart-man-illustration-avatar-icon-fashion-men-avatar-face-fashion-girl-thumbnail.png",
                "https://e7.pngegg.com/pngimages/340/946/png-clipart-avatar-user-computer-icons-software-developer-avatar-child-face-thumbnail.png",
                "https://e7.pngegg.com/pngimages/136/22/png-clipart-user-profile-computer-icons-girl-customer-avatar-angle-heroes-thumbnail.png"

            };
            string[] womenPath = { "https://e7.pngegg.com/pngimages/439/19/png-clipart-avatar-user-profile-icon-women-wear-frock-face-holidays-thumbnail.png",
                "https://e7.pngegg.com/pngimages/597/984/png-clipart-female-avatar-woman-avatar-purple-heroes-thumbnail.png",
                "https://e7.pngegg.com/pngimages/40/505/png-clipart-female-illustration-avatar-female-avatar-purple-face-thumbnail.png",
                "https://e7.pngegg.com/pngimages/732/397/png-clipart-computer-icons-avatar-woman-user-avatar-child-face-thumbnail.png",
                "https://e7.pngegg.com/pngimages/732/397/png-clipart-computer-icons-avatar-woman-user-avatar-child-face-thumbnail.png"
            };

            if (gender)
            {
                return womenPath[randomInRange];
            }
            else {
                return menPath[randomInRange];
            }

        }

        private bool containsUserName(string userName) {
            // Kullanıcı adı veya e-posta adresi ile ilgili kayıtları
            
            var existingUser = db.users.FirstOrDefault(u => u.userName == userName);

            // Eğer kayıt varsa, kullanıcı adı veya e-posta adresi kullanılmaktadır
            if (existingUser != null)
            {
                return true;
            }

            return false;
        }

        private bool containsEmail(string email)
        {
            // Kullanıcı adı veya e-posta adresi ile ilgili kayıtları

            var existingUser = db.users.FirstOrDefault(u => u.email == email);

            // Eğer kayıt varsa, kullanıcı adı veya e-posta adresi kullanılmaktadır
            if (existingUser != null)
            {
                return true;
            }

            return false;
        }

        private bool IsPasswordValid(string password)
        {
            // Şifre uygunluk kontrolü gerçekleştir ve sonucu döndür
            // Özel şifre uygunluk kurallarını burada uygulayabilirsiniz

            // Örnek kontrol: En az bir büyük harf, bir küçük harf ve bir rakam içermeli
            return password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);
        }

        private static void CreateUserFolder(string userName)
        {
            string baseFolderPath = @"/Users/macbook/Documents/GitHub/YemekTarifleri/YemekTarifleri/wwwroot/UserImages"; 
            string userFolderPath = Path.Combine(baseFolderPath, "UserImages", userName);

            if (!Directory.Exists(userFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(userFolderPath);
                    Console.WriteLine("Klasör başarıyla oluşturuldu.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Klasör oluşturma hatası: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Klasör zaten mevcut.");
            }
        }



    }
}


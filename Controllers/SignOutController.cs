using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YemekTarifleri.Controllers
{
    public class SignOutController : Controller
    {
        // GET: /<controller>/
        public IActionResult SignOut()
        {
            // Kullanıcının oturumunu sonlandırmak için ilgili cookie'yi siler
            Response.Cookies.Delete("userId");
            UserInfo.setUser(null);

            return RedirectToAction("Index", "Home");
        }
    }
}


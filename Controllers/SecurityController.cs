using MvcStok.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(kullanici kullanici)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kullanici.FirstOrDefault(x => x.KullaniciAd == kullanici.KullaniciAd && x.Parola == kullanici.Parola);
                if (model != null)
                {
                    FormsAuthentication.SetAuthCookie(model.KullaniciAd, false);
                    return RedirectToAction("Index", "Urun");
                }
                else
                {
                    ViewBag.Mesaj = "Kullanıcı adı veya Parola Yanlış";
                    return View();
                }
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}
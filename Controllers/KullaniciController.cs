using MvcStok.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace MvcStok.Controllers
{

    public class KullaniciController : Controller
    {
        // GET: Kullanici
        [Authorize]
        public ActionResult Index()
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kullanici.Include(x => x.yetki).Where(x => x.YetkiId > 1).ToList();
                return View(model);
            }

        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(kullanici kullanici)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kullanici.FirstOrDefault(x => x.KullaniciAd == kullanici.KullaniciAd);
                if (model == null)
                {
                    kullanici.YetkiId = 3;
                    db.kullanici.Add(kullanici);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Security");
                }
                else
                {
                    ViewBag.Mesaj = "Kullanıcı Adını Alamazsınız.";
                }
            }
            return View();
        }
        [Authorize]
        public ActionResult Sil(int id)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kullanici.Find(id);
                db.kullanici.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Kullanici");
            }
        }
        [Authorize]
        public ActionResult Yetki(int id)
        {
            int kullanici = 3;
            int admin = 2;
            using (DbDukkanEntities db = new DbDukkanEntities())
            {

                var model = db.kullanici.Find(id);

                if (model.YetkiId == admin)
                {
                    model.YetkiId = kullanici;
                    db.SaveChanges();
                }
                else if (model.YetkiId == kullanici)
                {
                    model.YetkiId = admin;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Kullanici");
            }
        }
        [Authorize]
        public ActionResult Sifre(string ad)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kullanici.FirstOrDefault(m => m.KullaniciAd == ad);
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult Sifre(kullanici kullanici)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                db.Entry(kullanici).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
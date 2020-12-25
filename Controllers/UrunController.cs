using MvcStok.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace MvcStok.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        // GET: Urun

        public ActionResult Index()
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.urun.Include(x => x.kategori).ToList();//Eager Loading....
                return View(model);
            }

        }
        [HttpGet]
        public ActionResult Ekle(string Ad)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                ViewBag.kategori = db.kategori.ToList();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(urun urun)
        {
            if (urun.Id == 0)
            {
                using (DbDukkanEntities db = new DbDukkanEntities())
                {
                    ViewBag.kategori = db.kategori.ToList();
                    db.urun.Add(urun);
                    db.SaveChanges();
                }
            }
            else
            {
                using (DbDukkanEntities db = new DbDukkanEntities())
                {
                    db.Entry(urun).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Urun");
        }
        [Authorize(Roles = "admin,süperadmin")]
        public ActionResult Guncelle(int id)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.urun.Find(id);
                if (model != null)
                {
                    ViewBag.kategori = db.kategori.ToList();
                    return View("Ekle", model);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }
        [Authorize(Roles = "admin,süperadmin")]
        public ActionResult Sil(int id)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.urun.Find(id);
                db.urun.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Urun");
            }
        }
    }
}
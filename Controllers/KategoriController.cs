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
    public class KategoriController : Controller
    {
        // GET: Kategori

        public ActionResult Index()
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kategori.Include(x => x.urun).ToList();//Eager Loading
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Ekle(string Ad)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(kategori kategori)
        {
            if (kategori.Id == 0)
            {
                using (DbDukkanEntities db = new DbDukkanEntities())
                {
                    db.kategori.Add(kategori);
                    db.SaveChanges();
                }
            }
            else
            {
                using (DbDukkanEntities db = new DbDukkanEntities())
                {
                    var model = db.kategori.Find(kategori.Id);
                    if (model != null)
                    {
                        model.Ad = kategori.Ad;
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                    db.SaveChanges();
                }
            }



            return RedirectToAction("Index", "Kategori");
        }
        [Authorize(Roles = "admin,süperadmin")]
        public ActionResult Guncelle(int id)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.kategori.Find(id);
                if (model != null)
                {
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
                var model = db.kategori.Include(x => x.urun).FirstOrDefault(x => x.Id == id);
                if (model != null)
                {
                    if (model.urun.Count > 0)
                    {
                        var urunler = db.urun.Include(x => x.kategori).Where(x => x.KategoriId == id).ToList();
                        return View(urunler);
                    }
                    else
                    {
                        db.kategori.Remove(model);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Kategori");
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }
        [Authorize(Roles = "admin,süperadmin")]
        public ActionResult DoluKategoriSil(int id)
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.urun.Where(x => x.KategoriId == id).ToList();
                for (int i = 0; i < model.Count; i++)
                {
                    db.urun.Remove(model[i]);
                }

                db.SaveChanges();
                Sil(id);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
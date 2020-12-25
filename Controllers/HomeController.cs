using MvcStok.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace MvcStok.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (DbDukkanEntities db = new DbDukkanEntities())
            {
                var model = db.urun.Include(x => x.kategori).Where(x => x.SatistaMi == true).ToList();//Eager Loading....
                return View(model);
            }

        }

    }
}
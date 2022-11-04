using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            //müşterilerden d verisine göre çekip d ye atıcak.
            var degerler = from d in db.Tbl_Musteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                //boş değilse bunu yap
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            //boşsa bunu yap
            return View(degerler.ToList());
            //var degerler = db.Tbl_Musteriler.ToList();
            //return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Tbl_Musteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_Musteriler.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult MusteriGetir(int id)
        {
            var must = db.Tbl_Musteriler.Find(id);
            return View("MusteriGetir",must);
        }

        public ActionResult Guncelle(Tbl_Musteriler p1)
        {
            var musteri = db.Tbl_Musteriler.Find(p1.MUSTERIID);
            musteri.MUSTERIAD = p1.MUSTERIAD;
            musteri.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
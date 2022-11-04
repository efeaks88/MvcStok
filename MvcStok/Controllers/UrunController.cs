using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
//modeli ekliyoruz
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        //yine aynı şekilde çağırıyoruz.
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Urunler.ToList();

            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            //aşağıdaki yazı linq sorgulaması
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                            }).ToList();
            //aşağıdaki komut controller tarafındaki nesneyi diğer html dosyasında kullanmamıza yarıyor.
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Tbl_Urunler p1)
        {
            var ktg = db.Tbl_Kategoriler.Where(m => m.KATEGORIID == p1.Tbl_Kategoriler.KATEGORIID).FirstOrDefault();
            p1.Tbl_Kategoriler = ktg;
            db.Tbl_Urunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SIL(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(Tbl_Urunler p)
        {
            var urun = db.Tbl_Urunler.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.Stok = p.Stok;
            urun.FİYAT = p.FİYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.Tbl_Kategoriler.Where(m => m.KATEGORIID == p.Tbl_Kategoriler.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
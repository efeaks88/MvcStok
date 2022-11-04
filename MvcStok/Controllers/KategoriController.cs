using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//mvc stok modelini çağırdık 14.satırı kullanabilmek için
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        //tablolara ulaşmak için db nesnesini ürettik.
        //mvcdbstokentities bizim tabloları tutuypr ordan db nesnesi oluşturudk.
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int Sayfa=1 )
        {
            //var degerler = db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList().ToPagedList(Sayfa, 5);
            return View(degerler);

        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            //eğer siteyi açtım ama bişi yapmadım view e geri döndür bişi yapma
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategoriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            //sitede bişr işlem yaparsam post işlemi gerçekleşir.
            db.Tbl_Kategoriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.Tbl_Kategoriler.Find(id);
            db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr=db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir",ktgr);
        }
        public ActionResult Guncelle(Tbl_Kategoriler p1)
        {
            var ktg = db.Tbl_Kategoriler.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
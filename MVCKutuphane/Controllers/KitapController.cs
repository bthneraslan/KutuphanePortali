using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(string p)
        {
            
            var kitaplar = db.TBLKITAP.Where(x => x.DURUM == true);
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(x => x.AD.Contains(p) || x.TBLYAZAR.AD.Contains(p) || 
                x.TBLKATEGORI.AD.Contains(p) || x.BASIMYIL.Contains(p) || x.YAYINEVI.Contains(p) 
                && x.DURUM == true);
            }
            return View(kitaplar.ToList());
        }

        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(TBLKITAP p1)
        {
            var kitapbul = db.TBLKITAP.Find(p1.ID);
            kitapbul.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapGeriGetir(TBLKITAP p2)
        {
            var kitapbul = db.TBLKITAP.Find(p2.ID);
            kitapbul.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SilinenKitapGetir()
        {
            var kitaplar = db.TBLKITAP.Where(z => z.DURUM == false).ToList();
            return View(kitaplar);
        }
        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View("KitapGetir", ktp);

        }
        public ActionResult KitapGuncelle(TBLKITAP p)
        {
            var ktp = db.TBLKITAP.Find(p.ID);
            ktp.AD = p.AD;
            ktp.BASIMYIL = p.BASIMYIL;
            ktp.SAYFA = p.SAYFA;
            ktp.YAYINEVI = p.YAYINEVI;
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            var yzr = db.TBLKATEGORI.Where(k => k.ID == p.TBLYAZAR.ID).FirstOrDefault();
            ktp.YAZAR = yzr.ID;
            ktp.KATEGORI = ktg.ID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }

}
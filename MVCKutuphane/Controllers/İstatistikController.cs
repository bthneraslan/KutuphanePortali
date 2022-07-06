using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class İstatistikController : Controller
    {
        // GET: İstatistik
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBLKITAP.Count();
            ViewBag.dgr2 = deger2;
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult Hava()
        {
            return View();
        }

        public ActionResult HavaKartlari()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/Galeri/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

        public ActionResult LinqKartlar()
        {
            var deger1 = db.TBLKITAP.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBLUYELER.Count();
            ViewBag.dgr2 = deger2;
            var deger3 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.dgr4 = deger4;
            var deger5 = db.TBLKATEGORI.Count();
            ViewBag.dgr5 = deger5;
            var deger11 = db.TBLILETISIM.Count();
            ViewBag.dgr11 = deger11;
            var deger8 = db.enfazlakitapyazar3().FirstOrDefault();
            ViewBag.dgr8 = deger8;
            var deger9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z => z.Count()).Select(y => new { y.Key }).FirstOrDefault();
            ViewBag.dgr9 = deger9;

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesaj = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail.ToString()).ToList();

            var d1 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;

            var d2 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;

            return View(mesaj);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyemail = (string)Session["Mail"].ToString();

            t.GONDEREN = uyemail.ToString();
            t.TARIH = DateTime.Parse( DateTime.Now.ToShortDateString());
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("GidenMesajlar");
        }

        public ActionResult GidenMesajlar()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesaj = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail.ToString()).ToList();

            var d1 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;

            var d2 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;

            return View(mesaj);
        }
        
        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"].ToString();

            var d1 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;

            var d2 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;

            var d8 = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count();
            ViewBag.d8 = d8;

            var d9 = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.d9 = d9;
            return PartialView();
        }

        public ActionResult MesajGetir (int id)
        {

            var uye = db.TBLMESAJLAR.Find(id);
            return View("MesajGetir", uye);
        }

        public ActionResult MesajGetir2(int id)
        {
            var uye = db.TBLMESAJLAR.Find(id);
            return View("MesajGetir2", uye);


            //var kullanici = (string)Session["Mail"];
            //var mesaj = db.TBLUYELER.Where(x => x.MAIL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            //var har = db.TBLMESAJLAR.Where(k => k.ID == id).ToList();
            //return View(har);
        }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    [Authorize(Roles ="A")]
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var har = db.TBLHAREKET.Where(k => k.ISLEMDURUM == false).ToList();
            return View(har);
        }

        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from i in db.TBLUYELER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + " " +i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from i in db.TBLPERSONEL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.PERSONEL,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET h)
        {
            var d1 = db.TBLUYELER.Where(k => k.ID == h.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKITAP.Where(k => k.ID == h.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(k => k.ID == h.TBLPERSONEL.ID).FirstOrDefault();
            h.TBLUYELER = d1;
            h.TBLKITAP = d2;
            h.TBLPERSONEL = d3;
            db.TBLHAREKET.Add(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.dgr = d3.TotalDays;
            return View("Odunciade", odn);
        }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrkt = db.TBLHAREKET.Find(p.ID);
            hrkt.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrkt.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
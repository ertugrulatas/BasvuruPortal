using BasvuruPortal.Filter;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BasvuruPortal.Security;

namespace BasvuruPortal.Controllers
{
    [LoginFilter]
    [RoleFilter(Roles = "1,3")]
    public class OgrislController : Controller
    {

        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TekdersSinavi()
        {
            var donem = db.TekDersDonemis.Where(x => x.Durum == true).SingleOrDefault();
            if (donem == null)
            {
                return RedirectToAction("TekDersDonemler");
            }
            ViewBag.Donem = donem.TekDersDonemAdi;
            ViewBag.sinavTarihi = donem.SinavTarihi;
            ViewBag.BasvuruBaslamaTarihi = donem.BaslangicTarihi;
            ViewBag.BasvuruBitisTarihi = donem.BitisTarihi;
            var model = db.TekDersBasvurus.Where(x => x.TekDersDonem.Durum == true).ToList();
            ViewBag.SinavaKatilacakSayi = model.Count();
            ViewBag.DersOnaylayanSayi = model.Where(x => x.DersSecim == true).Count();
            ViewBag.DersOnayBekleyen = model.Where(x => x.DersSecim == false).Count();

            return View();
        }

        public ActionResult TekDersSinavListesi()
        {
            var model = db.TekDersBasvurus.Where(x => x.TekDersDonem.Durum == true).ToList();
            return View(model);
        }

        public ActionResult TekDersDonemler()
        {
            var model = db.TekDersDonemis.ToList();
            return View(model);
        }

        public ActionResult TekDersSinavDonemDuzenle(int Id)
        {
            var model = db.TekDersDonemis.Find(Id);
            ViewBag.BaslangicTarihi = model.BaslangicTarihi;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TekDersSinavDonemDuzenle(TekDersDonemi model, string Durum)
        {
            var donem = db.TekDersDonemis.Find(model.Id);
            if (Durum == "on")
            {
                var _donem = db.TekDersDonemis.Where(x => x.Durum == true).FirstOrDefault();
                if (_donem != null)
                {
                    TempData["Mesaj"] = "Sistemde açık dönem olduğu için  dönem kapalı olarak kaydedilmiştir. Lütfen önceki açık olan dönemi kapatın.";
                }
                else
                {
                    donem.Durum = true;
                }
            }
            else
            {
                donem.Durum = false;
            }

            donem.BaslangicTarihi = model.BaslangicTarihi;
            donem.BitisTarihi = model.BitisTarihi;
            donem.SinavTarihi = model.SinavTarihi;
            donem.TekDersDonemAdi = model.TekDersDonemAdi;
            donem.TekdersDonemNo = model.TekdersDonemNo;
            donem.TekdersYili = model.TekdersYili;

            db.Entry(donem).State = EntityState.Modified;
            db.SaveChanges();

            TempData["Success"] = "Başarıyla Güncellendi";

            LogKayit.LogData(Session["SicilNo"].ToString(), null, HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Request.UserHostAddress, "TekDersSinavDönemDüzenleme", 3);

            return RedirectToAction("TekDersDonemler");
        }

        public ActionResult TekDersSinavDonemOlustur()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TekDersSinavDonemOlustur(TekDersDonemi model, string Durum)
        {
            TekDersDonemi _newdonem = new TekDersDonemi();
            _newdonem.BaslangicTarihi = model.BaslangicTarihi;
            _newdonem.BitisTarihi = model.BitisTarihi;
            _newdonem.SinavTarihi = model.SinavTarihi;
            _newdonem.TekdersYili = model.TekdersYili;
            _newdonem.TekdersDonemNo = model.TekdersDonemNo;
            _newdonem.TekDersDonemAdi = model.TekDersDonemAdi;

            if (Durum == "on")
            {
                var _donem = db.TekDersDonemis.Where(x => x.Durum == true).FirstOrDefault();
                if (_donem != null)
                {
                    _newdonem.Durum = false;
                    TempData["Mesaj"] = "Sistemde açık dönem olduğu için yeni oluşturulan dönem kapalı olarak kaydedilmiştir. Lütfen önceki açık olan dönemi kapatın.";
                }
                else
                {
                    _newdonem.Durum = true;
                }
            }
            else
            {
                _newdonem.Durum = false;
            }

            db.TekDersDonemis.Add(_newdonem);
            db.SaveChanges();
            TempData["Success"] = "Yeni dönem başarılı olarak oluşturulmuştur";
            LogKayit.LogData(Session["SicilNo"].ToString(), null, HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Request.UserHostAddress, "TekDersSinavDonemOlusturma", 3);

            return RedirectToAction("TekDersDonemler");
        }

        public ActionResult TekdersOgrenciEkle()
        {
            var _donem = db.TekDersDonemis.Where(x => x.Durum == true);
            if (_donem == null)
            {
                TempData["Mesaj"] = "Tek ders Dönemi aktif edilmede Öğrenci Ekleme açılamaz";
            }

            ViewBag.TekDersDonemId = new SelectList(_donem, "Id", "TekDersDonemAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TekdersOgrenciEkle(TekDersBasvuru model)
        {
            var _donem = db.TekDersDonemis.Where(x => x.Durum == true);
            var donems=_donem.Select(x => x.Id).FirstOrDefault();
            var _ogrenci = db.TekDersBasvurus.Where(x => x.OgrenciNo == model.OgrenciNo && x.OgrenciTCKNo==model.OgrenciTCKNo && (Int32)model.TekDersDonemId==donems).SingleOrDefault();
            if ((model.OgrenciNo != null) && (model.OgrenciTCKNo != null))
            {
                if (_ogrenci == null)
                {
                    TekDersBasvuru _newogrenci = new TekDersBasvuru();
                    _newogrenci.BolumAdi = model.BolumAdi;
                    _newogrenci.BolumKodu = model.BolumKodu;
                    _newogrenci.DersAdi = model.DersAdi;
                    _newogrenci.DersKodu = model.DersKodu;
                    _newogrenci.FakulteAdi = model.FakulteAdi;
                    _newogrenci.FakulteKodu = model.FakulteKodu;
                    _newogrenci.OgrenciAdi = model.OgrenciAdi;
                    _newogrenci.OgrenciAdres = model.OgrenciAdres;
                    _newogrenci.OgrenciEmail = model.OgrenciEmail;
                    _newogrenci.OgrenciNo = model.OgrenciNo;
                    _newogrenci.OgrenciSoyadi = model.OgrenciSoyadi;
                    _newogrenci.OgrenciTCKNo = model.OgrenciTCKNo;
                    _newogrenci.OgrenciTelefon = model.OgrenciTelefon;
                    _newogrenci.SinavTarihi = _donem.Select(x => x.SinavTarihi).FirstOrDefault();
                    _newogrenci.TekDersDonemId = model.TekDersDonemId;
                    db.TekDersBasvurus.Add(_newogrenci);
                    db.SaveChanges();
                    TempData["Success"] = "Öğrenci Başarıyla Eklendi";
                    return RedirectToAction("TekDersSinavListesi");
                }
                else
                {
                    TempData["Error"] = "Bu Öğrenci daha önceden kayıt yapılmıştır. Bu nedenle kayıt yapılamaz";
                }

            }
            else
            {
                TempData["Error"] = "Zorunlu alanlar boş geçilemez";
            }
          
            ViewBag.TekDersDonemId = new SelectList(_donem, "Id", "TekDersDonemAdi");

            return View();
        }

    }
}
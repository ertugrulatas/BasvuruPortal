using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class PersonelController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Personel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult emailbasvuru()
        {
            ViewBag.Baslik = "ELEKTRONİK POSTA HESABI BAŞVURU FORMU";
            PersonelEmail _model = new PersonelEmail();
            ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
            return View(_model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult emailbasvuru(PersonelEmail model)
        {
            var _sicil = db.personelEmails.SingleOrDefault(x => x.SicilNo == model.SicilNo);
            if (_sicil != null)
            {
                TempData["hata"] = "Bu sicil numarasına daha önceden başvuru yapılmıştır. <br/> Lütfen Bilgi İşlem Daire Başkanlığı ile görüşünüz";

                ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
                return View(model);
            }
            if (ModelState.IsValid)
            {

                PersonelEmail _newemail = new PersonelEmail();
                _newemail.AdiSoyadi = model.AdiSoyadi;
                _newemail.BasvuruTarihi = DateTime.Now;
                _newemail.birimi = model.birimi;
                _newemail.CepTelefonu = model.CepTelefonu;
                _newemail.isTelefonu = model.isTelefonu;
                _newemail.MailAdresi = model.MailAdresi;
                _newemail.sifre = model.sifre;
                _newemail.SicilNo = model.SicilNo;
                _newemail.statuID = model.statuID;
                if (model.StatuDiger != "")
                {
                    _newemail.StatuDiger = model.StatuDiger;
                }
                _newemail.TCKimlikNo = model.TCKimlikNo;
                _newemail.Unvan = model.Unvan;
                _newemail.uyruk = model.uyruk;
                _newemail.Guid = Convert.ToString(Guid.NewGuid());
                db.personelEmails.Add(_newemail);
                db.SaveChanges();
                TempData["mesaj"] = "Başvurunuz alınmıştır. <br/> Başvurunuzun onaylanması ve Email Hesabınızın açılması için Lütfen bu formun çıktısını alıp imzalayarak başvuru sayfasında yer alan İmzalı Başvuru Formunu Yükle butonu ile sisteme yükleyiniz.";
                return RedirectToAction("Yazdir", new { _newemail.Guid });
            }
            ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
            return View(model);
        }

        public ActionResult Yazdir(string Guid)
        {
            PersonelEmail _bul = db.personelEmails.SingleOrDefault(x => x.Guid == Guid);
            if (Guid == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            return View(_bul);
        }

        public ActionResult AracGirisKartiBasvuru()
        {
            ViewBag.Baslik = "ARAÇ GİRİŞ KARTI BAŞVURU FORMU";
            ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
            PersonelArac _model = new PersonelArac();
            return View(_model);
        }
     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AracGirisKartiBasvuru(PersonelArac model, HttpPostedFileBase file)
        {
            var _sicil = db.personelAracs.Where(x => x.SicilNo == model.SicilNo && x.onay<2).ToList();
            if (_sicil.Count==2)
            {
                TempData["hata"] = "Bu sicil numarasına daha önceden 2 kez başvuru yapılmıştır. <br/> Lütfen Bilgi İşlem Daire Başkanlığı ile görüşünüz";

                ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                PersonelArac _newarac = new PersonelArac();
                _newarac.AdiSoyadi = model.AdiSoyadi;
                _newarac.AracModeli = model.AracModeli;
                _newarac.AracRengi = model.AracRengi;
                _newarac.BasvuruTarihi = DateTime.Now;
                _newarac.birimi = model.birimi;
                _newarac.CepTelefonu = model.CepTelefonu;
                _newarac.Mailadres = model.Mailadres;
                _newarac.isTelefonu = model.isTelefonu;
                _newarac.ModelYili = model.ModelYili;
                _newarac.Plaka = model.Plaka;
                _newarac.RuhsatSahibi = model.RuhsatSahibi;
                _newarac.RuhsatSahibiYakinlik = model.RuhsatSahibiYakinlik;
                _newarac.SicilNo = model.SicilNo;
                _newarac.statuID = model.statuID;
                _newarac.TCKimlikNo = model.TCKimlikNo;
                _newarac.Unvan = model.Unvan;
                _newarac.uyruk = model.uyruk;
                _newarac.StatuDiger = model.StatuDiger;
                _newarac.Guid = Convert.ToString(Guid.NewGuid());
                if (file == null)
                {
                    ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
                    ModelState.AddModelError("ruhsatresim", "Lütfen aracın ruhsatının resmini çekip yükleyiniz");
                    return View(model);
                }

                if ((file.ContentType == "image/jpg") || (file.ContentType == "image/jpeg") || (file.ContentType == "image/png"))
                {
                    Stream str = file.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    _newarac.ruhsatresim = Br.ReadBytes((Int32)str.Length);
                }
                else
                {
                    ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
                    ModelState.AddModelError("ruhsatresim", "Sadece jpg, jpeg yada png türünde yükleyiniz");
                    return View(model);
                }
                db.personelAracs.Add(_newarac);
                db.SaveChanges();
                TempData["mesaj"] = "Başvurunuz alınmıştır. <br/> Başvurunuzun onaylanması için Lütfen bu formun çıktısını alıp imzalayarak başvuru sayfasında yer alan İmzalı Başvuru Formunu Yükle butonu ile sisteme yükleyiniz.";
                return RedirectToAction("AracFormYazdir", new { _newarac.Guid });
            }
            ViewBag.statuID = new SelectList(db.status.ToList(), "ID", "status");
            return View(model);
        }

        public ActionResult AracFormYazdir(string Guid)
        {
            PersonelArac _bul = db.personelAracs.SingleOrDefault(x => x.Guid == Guid);
            if (Guid == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            return View(_bul);
        }

        public ActionResult EmailFormGorseli()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailFormGorseli(PersonelEmailFormViewsModel model, HttpPostedFileBase dosya)
        {

            if (ModelState.IsValid)
            {
                var _personel = db.personelEmails.SingleOrDefault(x => x.TCKimlikNo == model.TCKimlik && x.SicilNo == model.SicilNo);
                if (_personel == null)
                {
                    TempData["hata"] = "TC Kimlik No veya Sicil No Hatalı";
                    return View(model);
                }
                if (dosya != null)
                {
                    var type = dosya.ContentType;

                    if ((dosya.ContentType == "image/jpg") || (dosya.ContentType == "image/jpeg") || (dosya.ContentType == "image/png"))
                    {
                        Stream str = dosya.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        _personel.formimage = Br.ReadBytes((Int32)str.Length);
                        db.Entry(_personel).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["mesaj"] = "İmzalı Formun görsel yüklemesi Başarılıdır. <br/>Email hesabınız en kısa sürede açılacaktır. <br/> Formda imzanız olmadığı tespit edildiğinde, Email hesabınız açılmayacaktır";
                        return RedirectToAction("EmailFormGorseli");
                    }
                    else
                    {
                        ModelState.AddModelError("dosya", "Formun İmzalı Görseli Jpeg/PNG/JPG formatlarından biri olmalıdır");
                        return View(model);
                    }

                }

                ModelState.AddModelError("dosya", "Formun İmzalı Görselini Eklemediniz. Zorunludur");
            }
            return View(model);
        }

        public ActionResult FormYazdir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormYazdir(PersonelEmailFormViewsModel model)
        {
            if (ModelState.IsValid)
            {
                var _personel = db.personelEmails.SingleOrDefault(x => x.TCKimlikNo == model.TCKimlik && x.SicilNo == model.SicilNo);
                if (_personel == null)
                {
                    TempData["hata"] = "TC Kimlik No veya Sicil No Hatalı";
                    return View(model);
                }

                return RedirectToAction("Yazdir", new { _personel.Guid });

            }
            return View(model);
        }


        public ActionResult AracFormGorseli()
        {
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult AracFormGorseli(PersonelEmailFormViewsModel model, HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                var _personel = db.personelAracs.SingleOrDefault(x => x.TCKimlikNo == model.TCKimlik && x.SicilNo == model.SicilNo);
                if (_personel == null)
                {
                    TempData["hata"] = "TC Kimlik No veya Sicil No Hatalı";
                    return View(model);
                }
                if (dosya != null)
                {
                    var type = dosya.ContentType;

                    if ((dosya.ContentType == "image/jpg") || (dosya.ContentType == "image/jpeg") || (dosya.ContentType == "image/png"))
                    {
                        Stream str = dosya.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        _personel.formimage = Br.ReadBytes((Int32)str.Length);
                        db.Entry(_personel).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["mesaj"] = "İmzalı Formun görsel yüklemesi Başarılıdır. <br/> Formda imzanız olmadığı tespit edildiğinde, Araç girişi aktif hale getirilmeyecektir.";
                        return RedirectToAction("AracFormGorseli");
                    }
                    else
                    {
                        ModelState.AddModelError("dosya", "Formun İmzalı Görseli Jpeg/PNG/JPG formatlarından biri olmalıdır");
                        return View(model);
                    }

                }

                ModelState.AddModelError("dosya", "Formun İmzalı Görselini Eklemediniz. Zorunludur");
            }
            return View(model);
        }
    
        public ActionResult AracFormuYazdir()
        {

            return View();
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public ActionResult AracFormuYazdir(PersonelEmailFormViewsModel model)
        {

            if (ModelState.IsValid)
            {
                var _personel = db.personelAracs.SingleOrDefault(x => x.TCKimlikNo == model.TCKimlik && x.SicilNo == model.SicilNo);
                if (_personel == null)
                {
                    TempData["hata"] = "TC Kimlik No veya Sicil No Hatalı";
                    return View(model);
                }

                return RedirectToAction("AracFormYazdir", new { _personel.Guid });

            }
            return View(model);
        }

        public ActionResult PersonelAracGirisBasvuru()
        {
            ViewBag.Baslik = "ARAÇ GİRİŞ BAŞVURU FORMU";
            
            aracgirisViewModel _model = new aracgirisViewModel();
            return View(_model);
        }
    }
}
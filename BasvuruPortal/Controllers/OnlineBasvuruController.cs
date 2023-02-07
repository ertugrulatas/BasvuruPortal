using BasvuruPortal.BAL;
using BasvuruPortal.Filter;
using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.Models.Kaysem;
using BasvuruPortal.ObisisVeritabani;
using BasvuruPortal.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasvuruPortal.Controllers
{
    public class OnlineBasvuruController : Controller
    {
        // GET: OnlineBasvuru

        DatabaseContext db = new DatabaseContext();
        OBISIS_KAYSERIEntities obp = new OBISIS_KAYSERIEntities();
        public ActionResult TalepOneri()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TalepOneri(TalepOneri model)
        {
            if (ModelState.IsValid)
            {
                TalepOneri _newdata = new TalepOneri();
                _newdata = model;
                _newdata.BasvuruTarihi = DateTime.Now;
                _newdata.Durum = false;
                db.talepOneris.Add(_newdata);
                db.SaveChanges();
                string tip = "";
                if (model.Tip == "T")
                {
                    tip = "Talep";
                }
                else if (model.Tip == "O")
                {
                    tip = "Öneri";
                }
                else
                {
                    tip = "Şikayet";
                }
                string icerik = @"<p><b>Talep Tipi:</b> " + tip +
                    "<br/><b>Adı Soyadı:</b> " + model.BasvuruAdiSoyadi +
                    "<br/><b>EmailAdresi:</b> " + model.MailAdresi +
                    "<br/><b>Telefon:</b> " + model.Telefon +
                    "<br/><b>Adres:</b> " + model.Adresi +
                      "<br/><b>Konusu:</b> " + model.Konu +
                        "<br/><b>Mesajı:</b> " + model.Mesaj
                    ;
                MailService.TalepGonder("Web Sitemizden Gelen Talep/Öneri/Şikayet", icerik);

                TempData["mesaj"] = "Mesajınız Başarıyla İletilmiştir";
                return RedirectToAction("TalepOneri");
            }
            return View(model);
        }

        public ActionResult HataUsulYolsuzluk()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HataUsulYolsuzluk(HataUsulYolsuzluk model,string MailAdres)
        {
            if (ModelState.IsValid)
            {
                HataUsulYolsuzluk _newdata = new HataUsulYolsuzluk();
                _newdata = model;
                _newdata.BildirimTarihi = DateTime.Now;
                _newdata.Durum = false;
                db.hataUsulYolsuzluks.Add(_newdata);
                db.SaveChanges();
                string tip = "";
                if (model.Tip == "H")
                {
                    tip = "Hata";
                }
                else if (model.Tip == "U")
                {
                    tip = "Usulsüzlük";
                }
                else
                {
                    tip = "Yolsuzluk";
                }
                string icerik = @"<p><b>Talep Tipi:</b> " + tip +
                    "<br/><b>TC Kimlik No:</b> " + model.TCKimlikNo +
                    "<br/><b>Adı Soyadı:</b> " + model.BasvuruAdiSoyadi +
                    "<br/><b>Görev Yeri:</b> " + model.GorevYeri +
                    "<br/><b>EmailAdresi:</b> " + model.MailAdresi +
                    "<br/><b>Cep Telefonu:</b> " + model.CepTelefonu +
                    "<br/><b>İş Telefonu:</b> " + model.CepTelefonu +
                    "<br/><b>Adres:</b> " + model.Adresi +
                      "<br/><b>Konusu:</b> " + model.Konu +
                        "<br/><b>Mesajı:</b> " + model.Mesaj
                    ;
                // MailService.HataGonder("Web Sitemizden Gelen Hata/Usulsuzluk/Yolsuzluk", icerik);
                SendEmail.SendMail(icerik, MailAdres, "Hata/Usulsuzluk/Yolsuzluk Bildirimi");

                TempData["mesaj"] = "Mesajınız Başarıyla İletilmiştir";
                return RedirectToAction("HataUsulYolsuzluk");
            }
            return View(model);
        }

        public ActionResult KaysemBasvuru()
        {
            ViewBag.Kurs_ID = new SelectList(db.kaysemKurss.Where(x => x.Durum == true).ToList().OrderBy(x => x.Kurs_Adi), "ID", "Kurs_Adi");
            ViewBag.MeslekID = new SelectList(db.kaysemMesleks.ToList().OrderBy(x => x.Meslek_Adi), "ID", "Meslek_Adi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KaysemBasvuru(KaysemBasvuru model, HttpPostedFileBase basvuru_formu, HttpPostedFileBase dekont,  HttpPostedFileBase diger_belgeler, string mailadres)
        {
            if (ModelState.IsValid)
            {
                var kursadi = db.kaysemBasvurus.Find(model.ID);
                KaysemBasvuru _newdata = new KaysemBasvuru();
                _newdata = model;
                if (basvuru_formu != null)
                {
                    Guid guid = Guid.NewGuid();
                    String FileExt3 = Path.GetExtension(basvuru_formu.FileName);
                    string _yenidosyaadi = guid.ToString() + "." + FileExt3.Substring(1, 3);
                    var DosyaTr = FileExt3.Substring(1, 3);
                    if (DosyaTr.Contains("exe") || DosyaTr.Contains("bat") || DosyaTr.Contains("sys"))
                    {
                        TempData["Error"] = "Lütfen Geçerli bir dosya formatı yükleyiniz";
                        return View(model);
                    }
                    basvuru_formu.SaveAs(HttpContext.Server.MapPath("~/Files/kaysem/basvuru_formu/" + _yenidosyaadi));
                    _newdata.Basvuru_Formu = "~/Files/kaysem/basvuru_formu/" + _yenidosyaadi;
                }
                if (dekont != null)
                {
                    Guid guid = Guid.NewGuid();
                    String FileExt = Path.GetExtension(dekont.FileName);
                    string _yenidosyaad = guid.ToString() + "." + FileExt.Substring(1, 3);
                    var DosyaTur = FileExt.Substring(1, 3);
                    if (DosyaTur.Contains("exe") || DosyaTur.Contains("bat") || DosyaTur.Contains("sys"))
                    {
                        TempData["Error"] = "Lütfen Geçerli bir dosya formatı yükleyiniz";
                        return View(model);
                    }
                    dekont.SaveAs(HttpContext.Server.MapPath("~/Files/kaysem/dekont/" + _yenidosyaad));
                    _newdata.Dekont = "~/Files/kaysem/dekont/" + _yenidosyaad;
                }
                if (diger_belgeler != null)
                {
                    Guid guid = Guid.NewGuid();
                    String FileExt2 = Path.GetExtension(diger_belgeler.FileName);
                    string _yenibelgead = guid.ToString() + "." + FileExt2.Substring(1, 3);
                    var DosyaTuru = FileExt2.Substring(1);
                    if (DosyaTuru.Contains("exe") || DosyaTuru.Contains("bat") || DosyaTuru.Contains("sys"))
                    {
                        TempData["Error"] = "Lütfen Geçerli bir dosya formatı yükleyiniz";
                        return View(model);
                    }
                    diger_belgeler.SaveAs(HttpContext.Server.MapPath("~/Files/kaysem/diger_belgeler/" + _yenibelgead));
                    _newdata.Diger_Belgeler = "~/Files/kaysem/diger_belgeler/" + _yenibelgead;
                }
                _newdata.BasvuruTarihi = DateTime.Now;
                _newdata.Durum = true;
                _newdata.Guid = Convert.ToString(Guid.NewGuid());
                db.kaysemBasvurus.Add(_newdata);
                db.SaveChanges();             

                var kursname = db.kaysemKurss.Find(model.KursID);
                //Mail Gönderimi KAYSEM Yetkilisi
                string link = "bidbbasvuru.kayseri.edu.tr/OnlineBasvuru/Kaysembasvurular";
                string subject = "KAYSEM Web Sitesi Online Kurs Başvurusu";
                string body = model.BasvuruAdiSoyadi + " tarafından KAYSEM web sitesi online başvuru sistemi üzerinden" + " " + kursname.Kurs_Adi + " kursu için başvuru yapıldı. <br/> " + "<a href=\"" + link + "\"> burayı tıklayarak  detayları görüntüleyebilirsiniz. <a>";
                string mail = mailadres;

                MailService.bildirimmail(mail, subject, body);

                //Mail Gönderimi KAYSEM Başvuru Yapan Kişi
                string subjectb = "Kayseri Üniversitesi Sürekli Eğitim Uygulama ve Araştırma Merkezi Online Kurs Başvurusu";
                string bodyb = "Kayseri Üniversitesi Sürekli Eğitim Uygulama ve Araştırma Merkezi online başvuru sistemi üzerinden " + " " + kursname.Kurs_Adi + " kursu için online başvurunuz alınmıştır.Detaylı bilgi için KAYSEM yetkilileri ile irtibata geçebilirsiniz.";
                string mailb = model.MailAdresi;

                MailService.bildirimmail(mail, subject, body);
                MailService.bildirimmail(mailb, subjectb, bodyb);
                TempData["mesaj"] = "Başvurunuz Alınmıştır.";
                return RedirectToAction("KaysemBasvuru");
            }
            ViewBag.Kurs_ID = new SelectList(db.kaysemKurss.Where(x => x.Durum == true).ToList().OrderBy(x => x.Kurs_Adi), "ID", "Kurs_Adi");
            ViewBag.MeslekID = new SelectList(db.kaysemMesleks.ToList().OrderBy(x => x.Meslek_Adi), "ID", "Meslek_Adi");

            return View(model);
        }

        [LoginFilter]
        [RoleFilter(Roles = "1,5")]
        public ActionResult Kaysembasvurular()
        {
            var model = db.kaysemBasvurus.Where(x => x.Durum == true).ToList().OrderByDescending(x => x.ID);
            return View(model);
        }

        [LoginFilter]
        [RoleFilter(Roles = "1,5")]
        public ActionResult Kaysembasvurudetay(int id)
        {
            var Kaysembasvurudetay = db.kaysemBasvurus.Find(id);

            return View(Kaysembasvurudetay);
        }

        public FileResult DownloadBasvuruFormu(int id)
        {

            var _dosya = db.kaysemBasvurus.Find(id);

            var FileVirtualPath = _dosya.Basvuru_Formu;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

        }
        public FileResult DownloadDekont(int id)
        {

            var _dosya = db.kaysemBasvurus.Find(id);

            var FileVirtualPath = _dosya.Dekont;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

        }
        public FileResult DownloadDigerBelgeler(int id)
        {

            var _dosya = db.kaysemBasvurus.Find(id);

            var FileVirtualPath = _dosya.Diger_Belgeler;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

        }

        public ActionResult KaysemBasvuruOnayla(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var _kaysembasvuru = db.kaysemBasvurus.SingleOrDefault(x => x.ID == id);
            if (_kaysembasvuru == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            _kaysembasvuru.Onay = true;
            db.Entry(_kaysembasvuru).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("KaysemBasvurular", "OnlineBasvuru"); ;
        }
        public ActionResult KaysemBasvuruSil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var _kaysembasvuru = db.kaysemBasvurus.SingleOrDefault(x => x.ID == id);
            if (_kaysembasvuru == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            _kaysembasvuru.Durum = false;
            db.Entry(_kaysembasvuru).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("KaysemBasvurular", "OnlineBasvuru"); ;
        }

       
    }
}
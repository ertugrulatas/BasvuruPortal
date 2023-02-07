using BasvuruPortal.Filter;
using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    [LoginFilter]
    [RoleFilter(Roles = "1,2")]
    public class YonetimController : Controller
    {
        // GET: Yonetim
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            ViewBag.EmailOnay = db.personelEmails.Where(x => x.onay == 0).Count();
            ViewBag.AracOnay = db.personelAracs.Where(x => x.onay == 0).Count();
            return View();
        }

        public ActionResult MailOnayBekleyenler()
        {
            var mailonay = db.personelEmails.Where(x => x.onay == 0).OrderBy(x => x.ID).ToList();
            return View(mailonay);
        }

        public ActionResult MailOnaylananlar()
        {
            var mailonay = db.personelEmails.Where(x => x.onay == 1).OrderByDescending(x => x.ID).ToList();
            return View(mailonay);
        }
        public ActionResult MailBasvuruOnayla(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var _mail = db.personelEmails.SingleOrDefault(x => x.ID == id);
            if (_mail == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(_mail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MailBasvuruOnayla(PersonelEmail model, HttpPostedFileBase file)
        {
            //if (file == null)
            //{
            //    ModelState.AddModelError("resimyok", "Lütfen Başvurunun imzalı Görselini Yükleyiniz");
            //    return View();
            //}
            if (model.MailAdresi == null)
            {
                TempData["hata"] = "Kullanıcıya tanımlanan mail adresi alanı boş bırakılamaz";
                return View(model);
            }
            var _mail = db.personelEmails.SingleOrDefault(x => x.ID == model.ID);
            if (_mail == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            _mail.MailAdresi = model.MailAdresi + "@kayseri.edu.tr";
            if (file != null)
            {
                Stream str = file.InputStream;
                BinaryReader Br = new BinaryReader(str);
                _mail.formimage = Br.ReadBytes((Int32)str.Length);
            }
            _mail.OnaylanayanKisi = Session["username"].ToString();
            _mail.OnayTarihi = DateTime.Now;
            _mail.onay = 1;
            db.Entry(_mail).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("MailOnayBekleyenler");
        }

        public ActionResult MailBasvuruDetay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var _mail = db.personelEmails.SingleOrDefault(x => x.ID == id);
            if (_mail == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(_mail);
        }

        public ActionResult AracBasvuruOnayBekleyenler()
        {
            var mailonay = db.personelAracs.Where(x => x.onay == 0).OrderBy(x => x.ID).ToList();
            return View(mailonay);
        }

        public ActionResult AracBasvuruOnaylananlar()
        {
            var mailonay = db.personelAracs.Where(x => x.onay == 1).OrderByDescending(x => x.ID).ToList();
            return View(mailonay);
        }


        public ActionResult AracBasvuruOnayla(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var _mail = db.personelAracs.SingleOrDefault(x => x.ID == id);
            if (_mail == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(_mail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AracBasvuruOnayla(PersonelArac model, HttpPostedFileBase file, string secim)
        {

            if ((secim == null) && (model.Aciklama == null))
            {
                var _model = db.personelAracs.SingleOrDefault(x => x.ID == model.ID);
                TempData["Error"] = "Lütfen açıklama alanına kabul etmeme nedenini yazınız";
                return View(_model);
            }

            var _arac = db.personelAracs.SingleOrDefault(x => x.ID == model.ID);
            if (_arac == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            string _body = "";
            _arac.KartID = model.KartID;
            _arac.OnaylanayanKisi = Session["username"].ToString();
            _arac.OnayTarihi = DateTime.Now;
            if ((secim != null) && secim.Contains("on"))
            {
                _arac.onay = 1;
                _arac.Aciklama = model.ID + " nolu Başvurunuz kabul edilmiştir.";
                _body = "Araç giriş Başvurunuz onaylanmıştır.\n Kayseri Üniversitesi Bilgi İşlem Daire Başkanlığı";
                if (file != null)
                {
                    Stream str = file.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    _arac.formimage = Br.ReadBytes((Int32)str.Length);
                }
            }
            else
            {
                _arac.onay = 2;
                _arac.Aciklama = model.Aciklama;
                _body = "Araç giriş Başvurunuz onaylanmamıştır. Açıklama : " + model.Aciklama;
            }

            db.Entry(_arac).State = EntityState.Modified;
            db.SaveChanges();
            SendEmail.SendMail(_body, _arac.Mailadres, "Araç Başvuru Durumu");
            return RedirectToAction("AracBasvuruOnayBekleyenler");
        }

        public ActionResult AracBasvuruDetay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            var _arac = db.personelAracs.SingleOrDefault(x => x.ID == id);
            if (_arac == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            return View(_arac);
        }


    }
}
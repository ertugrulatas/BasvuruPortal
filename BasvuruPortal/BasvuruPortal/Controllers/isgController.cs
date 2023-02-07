using BasvuruPortal.BAL;
using BasvuruPortal.Filter;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.Models.isguvenlik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class isgController : Controller
    {
        DatabaseContext db = new DatabaseContext();

      
        public ActionResult BildirimFormu(int id)
        {
            var form = db.isgBildirimTanims.Find(id);
            if (form == null)
            {
                return RedirectToAction("Error404", "Home");
            }
            ViewBag.Baslik = form.BildirimAdi;
            ViewBag.aciklama = form.Aciklama;
            ViewBag.BirimId = new SelectList(db.birimlers.ToList().OrderBy(x => x.BirimAdi), "ID", "BirimAdi");
            isgBildirimler model = new isgBildirimler();
            model.BildirimTanimId = form.Id;

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BildirimFormu(isgBildirimler model, HttpPostedFileBase files)
        {
            var bildirimadi = db.isgBildirimTanims.Find(model.BildirimTanimId);
            model.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                isgBildirimler _new = new isgBildirimler();
                _new = model;
                if (files != null)
                {
                    String FileExt = Path.GetExtension(files.FileName);
                    var DosyaTur = FileExt.Substring(1);
                    files.SaveAs(HttpContext.Server.MapPath("~/Files/isg/" + files.FileName));
                    _new.DosyaAdi = files.FileName;
                    _new.DosyaUrl = "~/Files/isg/" + files.FileName;
                    _new.DosyaTuru = DosyaTur;
                }
                
                db.isgBildirimlers.Add(_new);
                db.SaveChanges();

                //Mail Gönderimi
                string link = "bidbbasvuru.kayseri.edu.tr/isg/bildirimler";
                string subject = bildirimadi.BildirimAdi;
                string body = model.AdSoyad + " tarafından" + " " + subject + " bildirimi yapıldı. <br/> " +"<a href=\""+ link + "\"> burayı tıklayarak  detayları görebilirsiniz <a>";
                string mail = bildirimadi.MailSentAdress;

                MailService.bildirimmail(mail, subject, body);
                //

                TempData["Success"] = "Bildiriminiz alınmıştır teşekkür ederiz";
                return RedirectToAction("BildirimFormu", model.BildirimTanimId);
            }
            TempData["Error"] = "Zorunlu alanları doldurmadınız veya Olayın Meydana Geldiği Birim Seçimini Yapmadınız. Listede birim yoksa Diğer( Listede olmayan) alanını seçiniz";
            ViewBag.BirimId = new SelectList(db.birimlers.ToList().OrderBy(x => x.BirimAdi), "ID", "BirimAdi");
            return View(model);
        }

        [LoginFilter]
        [RoleFilter(Roles = "1,4")]
        public ActionResult bildirimler()
        {
            var model = db.isgBildirimlers.ToList().OrderByDescending(x => x.Id);
            return View(model);
        }

        [LoginFilter]
        [RoleFilter(Roles = "1,4")]
        public ActionResult bildirimdetay(int id)
        {
            var bildirim = db.isgBildirimlers.Find(id);

            return View(bildirim);
        }

        public FileResult Download(int id)
        {

            var _dosya = db.isgBildirimlers.Find(id);

            var FileVirtualPath = _dosya.DosyaUrl;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));

        }
    }
}
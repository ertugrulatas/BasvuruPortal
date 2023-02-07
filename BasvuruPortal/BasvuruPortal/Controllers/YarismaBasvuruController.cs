using BasvuruPortal.Models.DAL;
using BasvuruPortal.Models.Yarismalar;
using BasvuruPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class YarismaBasvuruController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Illustrasyon()
        {
            var yarisma = db.Yarismas.Where(x => x.Durum == true && x.YarismaTurId == 1).SingleOrDefault();
            if (DateTime.Now < yarisma.BaslangicTarihi)
            {
                TempData["kapali"] = yarisma.YarismaAdi + " henüz başlamamamıştır. Lütfen duyuruda belirtilen tarihte başvurunuzu yapınız";
                return RedirectToAction("Kapali");
            }
            else if (DateTime.Now > yarisma.BitisTarihi)
            {
                TempData["kapali"] = yarisma.YarismaAdi + " tamamlanmıştır. Yarışmaya gösterdiğiniz ilgi için teşekkür ederiz";
                return RedirectToAction("Kapali");
            }
            IllustrasyonYarismaBasvuruViewModel model = new IllustrasyonYarismaBasvuruViewModel();
            model.YarismaId = yarisma.Id;
            model.YarismaBaslangicTarihi = yarisma.BaslangicTarihi.ToString("dd/MM/yyyy");
            model.YarismaBitisTarihi = yarisma.BitisTarihi.ToString("dd/MM/yyyy");
            model.YarismaAdi = yarisma.YarismaAdi;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Illustrasyon(IllustrasyonYarismaBasvuruViewModel model, HttpPostedFileBase file, bool? onaycheckbox)
        {


            if (onaycheckbox == true)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        String FileExt = Path.GetExtension(file.FileName);
                        var DosyaTur = FileExt.Substring(1, 3);
                        if (DosyaTur.Contains("exe") || DosyaTur.Contains("bat") || DosyaTur.Contains("sys"))
                        {
                            TempData["Error"] = "Lütfen Geçerli bir resim formatı yükleyiniz";
                            return View(model);
                        }

                        if (file.ContentLength <= 18874368)
                        {
                            YarismaBasvuru _newyarismaci = new YarismaBasvuru();
                            Guid guid = Guid.NewGuid();
                            _newyarismaci.Id = guid;
                            string _yenidosyaad = guid.ToString() + "." + FileExt.Substring(1, 3);
                            _newyarismaci.Adres = model.Adres;
                            _newyarismaci.BasvuruTarihi = DateTime.Now;
                            _newyarismaci.YarismaId = model.YarismaId;
                            _newyarismaci.YarismaciAdi = model.YarismaciAdi;
                            _newyarismaci.Yarismacibeyan = true;
                            _newyarismaci.YarismaciDogumTarihi = model.YarismaciDogumTarihi;
                            _newyarismaci.YarismaciEmal = model.YarismaciEmail;
                            _newyarismaci.YarismaciPrjeAciklama = model.YarismaciProjeAciklama;
                            _newyarismaci.YarismaciSoyadi = model.YarismaciSoyadi;
                            _newyarismaci.YarismaciTelefon = model.YarismaciTelefon;
                           
                            _newyarismaci.YarismaDosyaAdi = _yenidosyaad;
                            _newyarismaci.YarismaDosyaTuru = DosyaTur;

                            file.SaveAs(HttpContext.Server.MapPath("~/Files/yarisma/" + _yenidosyaad));
                            _newyarismaci.BasvuruTarihi = DateTime.Now;
                            _newyarismaci.YarismaDosyaYolu = "~/Files/yarisma/" + _yenidosyaad;
                            db.YarismaBasvurus.Add(_newyarismaci);
                            db.SaveChanges();
                            TempData["Success"] = "Yarışmaya başvurunuz başarılı şekilde yapılmıştır.<br/> Teşekkür ederiz";
                            return RedirectToAction("Illustrasyon");
                        }
                        TempData["Error"] = "Yarışma Kurallarında belirtilen dosya boyutunu aştınız.Lütfen istenilen boyutta yükleyiniz";
                        return View(model);
                    }
                    TempData["Error"] = "Dosya yüklemediniz. Lütfen yarışma için dosyanızı yükleyiniz";
                    return View(model);
                }
                TempData["Error"] = "Lütfen zorunlu alanları doldurunuz";
            }
            else
            {
                TempData["Error"] = "Telif Hakkı ve Diğer Hususlar hakkında mesajı onaylamadınız";
            }
           
            return View(model);
        }

        public ActionResult Kapali()
        {
            return View();
        }
    }
}
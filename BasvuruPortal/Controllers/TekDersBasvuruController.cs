using BasvuruPortal.Filter;
using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasvuruPortal.Controllers
{
    public class TekDersBasvuruController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: TekDersBasvuru

        //[LoginFilter]
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(HttpContext.Session["OgrNo"])))
            {

                return RedirectToAction("Giris");
            }
            string TCKNo = Session["TCKNo"].ToString();
            string OgrNo = Session["OgrNo"].ToString();
            var model = db.TekDersBasvurus.Where(x => x.OgrenciTCKNo == TCKNo && x.OgrenciNo == OgrNo).SingleOrDefault();
            return View(model);
        }

        public ActionResult Giris()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(string OgrNo, string TCKNo)
        {
            var donemaktif = db.TekDersDonemis.Where(x => x.Durum == true).SingleOrDefault();
            if ((donemaktif != null) && (donemaktif.SinavTarihi > DateTime.Now))
            {


                var ogr = db.TekDersBasvurus.Where(x => x.OgrenciTCKNo == TCKNo && x.OgrenciNo == OgrNo && x.TekDersDonemId == donemaktif.Id).FirstOrDefault();
                if (ogr != null)
                {

                    FormsAuthentication.SetAuthCookie(OgrNo, false);
                    Session.Clear();
                    Session.Add("OgrNo", OgrNo);
                    Session.Add("TCKNo", TCKNo);
                    Session.Timeout = 60;

                    return RedirectToAction("Index");
                }
                else  //Kullanıcı Durum Pasif ise
                {
                    TempData["Mesaj"] = "Tek ders sınav hakkınız bulunmamaktadır. Hata olduğunu düşünüyorsanız, Öğrenci İşleri Daire Başkanlığı ile görüşünüz";

                }
            }
            else
            {
                TempData["Mesaj"] = "Tek ders sınavı başvuru dönemi kapalıdır. Başvuru döneminde belirtilen tarihte sisteme giriş yapılabilir";
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TekDersSinavOnay(TekDersBasvuru model)
        {

            var ogr = db.TekDersBasvurus.Where(x => x.OgrenciNo == model.OgrenciNo && x.TekDersDonemId == model.TekDersDonemId).FirstOrDefault();
            if (ogr != null)
            {
                ogr.DersAlmaZamani = DateTime.Now;
                ogr.DersSecim = true;
                db.Entry(ogr).State = EntityState.Modified;
                db.SaveChanges();
                LogKayit.LogData(null, ogr.OgrenciNo, HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Request.UserHostAddress, "TekdersKatılımOnay", 3);


            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Basvuruiptal(TekDersBasvuru model)
        {

            var ogr = db.TekDersBasvurus.Where(x => x.OgrenciNo == model.OgrenciNo && x.TekDersDonemId == model.TekDersDonemId).FirstOrDefault();
            if (ogr != null)
            {
                ogr.DersAlmaZamani = null;
                ogr.DersSecim = false;
                db.Entry(ogr).State = EntityState.Modified;
                db.SaveChanges();
                LogKayit.LogData(null, ogr.OgrenciNo, HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Request.UserHostAddress, "TekdersKatılımİptali", 3);

            }
            return RedirectToAction("Index");
        }
    }
}
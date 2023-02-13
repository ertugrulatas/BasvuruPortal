using BasvuruPortal.Filter;
using BasvuruPortal.Models.DAL;
using System;
using System.Collections.Generic;
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
            string TCKNo = Session["TCKNo"].ToString() ;
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
            var ogr = db.TekDersBasvurus.Where(x => x.OgrenciTCKNo == TCKNo && x.OgrenciNo == OgrNo).FirstOrDefault();
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
                TempData["Mesaj"] = "Giriş Yetkiniz Yoktur. Öğrenci İşleri Daire Başkanlığı ile görüşünüz";
               
            }

            return View();
        }
    }
}
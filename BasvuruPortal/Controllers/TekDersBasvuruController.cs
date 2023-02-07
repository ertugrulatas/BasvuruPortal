using BasvuruPortal.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class TekDersBasvuruController : Controller
    {
        // GET: TekDersBasvuru

      //  [LoginFilter]
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult Giris()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(string OgrNo, string TCKimlik)
        {

            Session.Add("OgrNo", OgrNo);
            return View();
        }
    }
}
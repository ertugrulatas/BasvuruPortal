using BasvuruPortal.Filter;
using BasvuruPortal.ObisisVeritabani;
using BasvuruPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    [LoginFilter]
    [RoleFilter(Roles = "1,3")]
    public class obpbasvuruController : Controller
    {
        OBISIS_KAYSERIEntities obp = new OBISIS_KAYSERIEntities();
        public ActionResult Obpkullanici()
        {
            var kullanici = obp.OBP_KULLANICI.ToList().OrderByDescending(x=>x.KULLANICI_KODU);
          
            return View(kullanici);
        }
    }
}
using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using BasvuruPortal.Models.Kaysem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class YazdirController : Controller
    {
        // GET: Yazdir
        DatabaseContext db = new DatabaseContext();
        public ActionResult PersonelEmailYazdir(string Guid)
        {
            PersonelEmail _bul = db.personelEmails.SingleOrDefault(x => x.Guid == Guid);
            if (Guid == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            return View(_bul);
        }

        public ActionResult PersonelAracGirisYazdır(string Guid)
        {
            PersonelArac _bul = db.personelAracs.SingleOrDefault(x => x.Guid == Guid);
            if (Guid == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            return View(_bul);
        }

        public ActionResult KaysemBasvuruYazdir(string Guid)
        {
            KaysemBasvuru _bul = db.kaysemBasvurus.SingleOrDefault(x => x.Guid == Guid);
            if (Guid == null)
            {
                return RedirectToAction("Error404", "Home");
            }

            return View(_bul);
        }
    }
}
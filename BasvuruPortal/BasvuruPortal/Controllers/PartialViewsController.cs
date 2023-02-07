using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class PartialViewsController : Controller
    {
        // GET: PartialViews
        public ActionResult DataTableCss()
        {
            return View();
        }

        public ActionResult DataTableScript()
        {
            return View();
        }

        public ActionResult formCss()
        {
            return View();
        }

        public ActionResult formScript()
        {
            return View();
        }
    }
}
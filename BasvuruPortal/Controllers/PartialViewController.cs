using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Controllers
{
    public class PartialViewController : Controller
    {
        // GET: PartialView
        public PartialViewResult Slider()
        {
            return PartialView();
        }


        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }

        public PartialViewResult Modal()
        {
            return PartialView();
        }

        public PartialViewResult ModalError()
        {
            return PartialView();
        }

        public PartialViewResult MesajModal()
        {
            return PartialView();
        }

        public PartialViewResult OnayModal()
        {
            return PartialView();
        }
    }
}
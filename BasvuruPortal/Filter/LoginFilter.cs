using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasvuruPortal.Filter
{
    public class LoginFilter: FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["mail"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectResult("~/Home/Error505");
            } 

          


        }
    }
}
using BasvuruPortal.BAL;
using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasvuruPortal.Controllers
{
    public class AccountController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(string email, string pass)
        {
            string password = hashsifre.encrypt(pass);
            user _user = db.users.Where(x => x.eMail == email && x.pass == password).FirstOrDefault();
            if (_user == null)  // Kullanıcı Bulunmadıysa
            {
                TempData["Mesaj"] = "Kullanıcı Adı yada Şifre Hatalı";
                return View();
            }
            if (_user.durum == true)  // Kullanıcı Durum Aktif ise
            {

                FormsAuthentication.SetAuthCookie(_user.eMail, false);
                Session.Clear();
                Session.Add("giris", "true");
                Session.Add("username", _user.AdiSoyadi);
                Session.Add("userID", _user.ID);
                Session.Add("mail", _user.eMail);
                Session.Add("RolID", _user.RolID);
                Session.Timeout = 60;
                if (_user.RolID == 3)
                {
                    return RedirectToAction("Obpkullanici", "obpbasvuru");
                }
                else if (_user.RolID == 4)
                {
                    return RedirectToAction("bildirimler", "isg");
                }
                else if (_user.RolID == 5)
                {
                    return RedirectToAction("Kaysembasvurular", "OnlineBasvuru");
                }
                else
                {
                    return RedirectToAction("Index", "Yonetim");
                }

            }
            else  //Kullanıcı Durum Pasif ise
            {
                TempData["Mesaj"] = "Giriş Yetkiniz Yoktur. Sistem Yöneticiniz ile iletişime geçiniz";
                return View();
            }
        }

        public ActionResult Cikis()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Giris");
        }
    }
}
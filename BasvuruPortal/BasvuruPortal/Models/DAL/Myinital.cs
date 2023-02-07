using BasvuruPortal.BAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.DAL
{
    public class Myinital : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            List<Statu> statuslist = new List<Statu>()
            {
                new Statu {status="Akademik"},
                new Statu {status="Memur"},
                new Statu {status="Sözleşmeli(4/B)"},
                new Statu {status="İşçi"},
                new Statu {status="35.Madde"},
                new Statu {status="Diğer"},
            };
            foreach (var item in statuslist)
            {
                context.status.Add(item);
            }
            context.SaveChanges();

            //********************************************************////
            List<user> userlist = new List<user>()
            {
                new user{AdiSoyadi="Ertuğrul ATAŞ",durum=true,ID=Guid.NewGuid(),eMail="ertugrulatas@kayseri.edu.tr",pass=hashsifre.encrypt("qwe123"),RolID=1},
                new user{AdiSoyadi="Ayhan Renklier",durum=true,ID=Guid.NewGuid(),eMail="ayhan@kayseri.edu.tr",pass=hashsifre.encrypt("Arenklier*"),RolID=1},
                new user{AdiSoyadi="Salih Murat Gürbüz",durum=true,ID=Guid.NewGuid(),eMail="salih@kayseri.edu.tr",pass=hashsifre.encrypt("SGurbuz?"),RolID=1},


            };
            foreach (var item in userlist)
            {
                context.users.Add(item);
            }
            context.SaveChanges();
        }
    }
}
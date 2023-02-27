using BasvuruPortal.Models;
using BasvuruPortal.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Security
{

    public class LogKayit
    {
       

        public static void LogData(string sicilNo, string OgrenciNo, string IP, string Yapilanislem, byte Crud)
        {
            DatabaseContext db = new DatabaseContext();
            LogData _log = new LogData();
            _log.IPAdress = IP;
            _log.CRUD = Crud;
            _log.Yapilanislem = Yapilanislem;
            _log.OgrenciNo = OgrenciNo;
            _log.PersonelSicilNo = sicilNo;
            _log.islemTarihi = DateTime.Now;
            db.LogDatas.Add(_log);
            db.SaveChangesAsync();
            return;
        }
       
    }
}
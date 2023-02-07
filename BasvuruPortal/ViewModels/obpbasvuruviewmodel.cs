using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasvuruPortal.ViewModels
{
    public class obpbasvuruviewmodel
    {
        public int KullaniciKodu { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public bool Aktif { get; set; }
      
    }
}
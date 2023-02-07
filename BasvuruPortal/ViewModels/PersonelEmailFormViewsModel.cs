using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasvuruPortal.ViewModels
{
    public class PersonelEmailFormViewsModel
    {
        public int ID { get; set; } 

        [Required(ErrorMessage ="TC Kimlik No doldurulması zorunludur")]
        public string TCKimlik { get; set; }

        [Required(ErrorMessage = "Sicil No doldurulması zorunludur")]
        public string SicilNo { get; set; }

        public byte[] File { get; set; }
    }
}
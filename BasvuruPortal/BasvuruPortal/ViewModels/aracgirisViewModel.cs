using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasvuruPortal.ViewModels
{
    public class aracgirisViewModel
    {
        [Required(ErrorMessage = "TC Kimlik alanı boş geçilemez")]
        [MaxLength(11, ErrorMessage = "TC Kimlik 11 Karakterden fazla olamaz")]
        [MinLength(11, ErrorMessage = "TC Kimlik 11 Karakterden az olamaz")]
        public string TCKimlikNo { get; set; }


        [Required(ErrorMessage = "Sicil No alanı boş geçilemez")]
        public string SicilNo { get; set; }
    }
}
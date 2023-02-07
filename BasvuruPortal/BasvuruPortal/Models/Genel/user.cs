using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("Users")]
    public class user
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Adınız Soyadınız Zorunludur")]
        public string AdiSoyadi { get; set; }

        [Required(ErrorMessage = "Mail Adresiniz Zorunludur")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Geçerli bir Mail Adresi Değildir")]
        public string eMail { get; set; }

        [Required(ErrorMessage = "Şifre Zorunludur")]
        public string pass { get; set; } // şifre

     
        public bool durum { get; set; }

        public Nullable<byte> RolID { get; set; } //1-Süper Admin 2- Admin 3- Standart Üye
    }
}
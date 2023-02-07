using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("PersonelEmail")]
    public class PersonelEmail
    {
        public int ID { get; set; }

        public string MailAdresi { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı boş geçilemez")]
        public string AdiSoyadi { get; set; }

        [Required(ErrorMessage = "TC Kimlik alanı boş geçilemez")]
        [MaxLength(11, ErrorMessage = "TC Kimlik 11 Karakterden fazla olamaz")]
        [MinLength(11, ErrorMessage = "TC Kimlik 11 Karakterden az olamaz")]
        public string TCKimlikNo { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Şifreniz 8 Karakterden az olamaz")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*[A-Za-z])(?=.*[0-9])(?=.*\d)(?=.*[$@$!%*#?.&])[A-Za-z\d$@$!%*#?.&]{8,}$", ErrorMessage = "Geçersiz Şifre")]
        public string sifre { get; set; }

        [Required(ErrorMessage = "Sicil No alanı boş geçilemez")]
        public string SicilNo { get; set; }

        public string Unvan { get; set; }

        [Required(ErrorMessage = "Statu alanı boş geçilemez")]
        public int statuID { get; set; }

        public string StatuDiger { get; set; } //Diğer seçilince işlenecek
        public string Guid { get; set; }
        public string uyruk { get; set; }

        public string birimi { get; set; }

        public string isTelefonu { get; set; }

        public string CepTelefonu { get; set; }

        public DateTime BasvuruTarihi { get; set; }

        public string pass { get; set; }

        public Nullable<DateTime> OnayTarihi { get; set; }

        public string OnaylanayanKisi { get; set; }

        public byte onay { get; set; }

        public string Aciklama { get; set; }

        public byte[] formimage { get; set; }

        public virtual Statu statu { get; set; }
    }
}
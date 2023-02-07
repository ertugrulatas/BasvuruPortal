using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasvuruPortal.ViewModels
{
    public class IllustrasyonYarismaBasvuruViewModel
    {
        public Guid Id { get; set; }

        public int YarismaId { get; set; }

        [DisplayName("Adınız")]
        [Required(ErrorMessage ="Ad alanı zorunlu alandır")]
        public string YarismaciAdi { get; set; }

        [DisplayName("Soyadınız")]
        [Required(ErrorMessage = "Soyadı alanı zorunlu alandır")]
        public string YarismaciSoyadi { get; set; }

        [DisplayName("T.C. Kimlik No")]
        [Required(ErrorMessage = " TC Kimlik alanı boş geçilemez")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 karakterden oluşmalıdır")]
        public string TCKimlikNo { get; set; }

        [DisplayName("Adresiniz")]
        [Required(ErrorMessage = "Adres alanı zorunlu alandır")]
        public string Adres { get; set; }

        [DisplayName("Doğum Tarihiniz")]
        public DateTime YarismaciDogumTarihi { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage ="Geçersiz Email adresi")]
        [Required(ErrorMessage = "Email Adres alanı zorunlu alandır")]
        [DisplayName("E-Posta Adresiniz")]
        public string YarismaciEmail { get; set; }


        [DisplayName("Telefon Numaranız")]
        [Required(ErrorMessage = "Telefon alanı boş geçilemez")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Telefon Numaranız 11 karakterden ve 05********* şeklinde oluşmalıdır")]
        public string YarismaciTelefon { get; set; }

        [DisplayName("Proje Açıklaması")]
        [Required(ErrorMessage = "Açıklama boş geçilemez")]
        public string YarismaciProjeAciklama { get; set; }
        public bool Yarismacibeyan { get; set; }
     
        public string YarismaDosyaAdi { get; set; }
        public string YarismaDosyaYolu { get; set; }
        public string YarismaDosyaTuru { get; set; }

        public string YarismaBaslangicTarihi { get; set; }
        public string YarismaBitisTarihi { get; set; }

        public string YarismaAdi { get; set; }
    }
}
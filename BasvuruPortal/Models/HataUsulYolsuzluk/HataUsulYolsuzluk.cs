using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("HataUsulYolsuzluk")]
    public class HataUsulYolsuzluk
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Hata/Usülsüzlük/Yolsuzluk alanı seçilmesi zorunludur")]
        public string Tip { get; set; }  //T: Talep, O, Oneri,S:Şikayet

        [StringLength(maximumLength: 100)]
        public string Konu { get; set; }
        public string Mesaj { get; set; }        
        [Required(ErrorMessage = "TC Kimlik alanı boş geçilemez")]
        [MaxLength(11, ErrorMessage = "TC Kimlik 11 Karakterden fazla olamaz")]
        [MinLength(11, ErrorMessage = "TC Kimlik 11 Karakterden az olamaz")]
        public string TCKimlikNo { get; set; }
        public string BasvuruAdiSoyadi { get; set; }
        public string GorevYeri { get; set; }
        public string CepTelefonu { get; set; }

        public string MailAdresi { get; set; }

        public DateTime BildirimTarihi { get; set; }

        public string Adresi { get; set; }
        public string IsTelefonu { get; set; }

        public bool Durum { get; set; } // O devam ediyor 1 tamamlandı
    }
}
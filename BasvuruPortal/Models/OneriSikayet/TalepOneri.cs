using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("TalepOneri")]
    public class TalepOneri
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage ="Talep/Öneri/Şikayet alanı seçilmesi zorunludur")]
        public string Tip { get; set; }  //T: Talep, O, Oneri,S:Şikayet

        [StringLength(maximumLength:100)]
        public string Konu { get; set; }
        public string Mesaj { get; set; }

        public string  BasvuruAdiSoyadi { get; set; }

        public string Telefon { get; set; }

        public string MailAdresi { get; set; }

        public DateTime BasvuruTarihi { get; set; }

        public string Adresi { get; set; }

        public bool Durum { get; set; } // O devam ediyor 1 tamamlandı

    }
}
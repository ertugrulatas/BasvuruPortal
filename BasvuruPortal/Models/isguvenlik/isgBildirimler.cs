using BasvuruPortal.Models.Genel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.isguvenlik
{
    [Table("isgBildirimler")]
    public class isgBildirimler
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BildirimTanimId { get; set; }

        [Required(ErrorMessage ="Boş geçilemez"),MaxLength(100)]
        public string AdSoyad { get; set; } // bildirim yapan

        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz Mail Adresi")]
        public string Email { get; set; }

        public string Telefon { get; set; }

        public int BirimId { get; set; } //Olayın meydana geldiği birim İdari- akademik

        [MaxLength(150)]
        public string DigerBirim { get; set; }// Olay farklı bir yerde meyda geldiyse

        public DateTime Tarih { get; set; } // Olayın meydana geldiği tarih

        public string Saat { get; set; } //Olayın meydana geldiği saat

        [Required(ErrorMessage = "Boş geçilemez")]
        public string Aciklama { get; set; } //Olayın açıklaması

       public DateTime CreateDate { get; set; }

        public string DosyaAdi { get; set; }
        public string DosyaUrl { get; set; }
        public string DosyaTuru { get; set; }

        public bool MailSent { get; set; }

        public virtual isgBildirimTanim BildirimTanim { get; set; }

        public virtual Birimler Birim { get; set; }
     }

}
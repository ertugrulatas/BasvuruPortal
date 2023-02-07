using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.isguvenlik
{
    [Table("isgBildirimTanim")]
    public class isgBildirimTanim
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string BildirimAdi { get; set; }// Ramak Kala bildirim- TEhlike Bildirim

        [DataType(DataType.EmailAddress, ErrorMessage = "Geçersiz Mail Adresi")]
        public string MailSentAdress { get; set; }//  İşlem yapıldığı zaman gönderilecek mail adresi

        public string Aciklama { get; set; }
        public bool Durum { get; set; }

        public string Url { get; set; }
        public ICollection<isgBildirimler> isgBildirimlers { get; set; } = new HashSet<isgBildirimler>();
    }
}
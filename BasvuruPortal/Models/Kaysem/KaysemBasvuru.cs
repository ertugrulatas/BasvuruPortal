using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Kaysem
{
    [Table("KaysemBasvuru")]
    public class KaysemBasvuru
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Kurs Seçim alanı boş geçilemez!")]
        public int KursID { get; set; }  
        [StringLength(maximumLength: 100)]
        public string Cinsiyet { get; set; }
        [Required(ErrorMessage = "TC Kimlik alanı boş geçilemez!")]
        [MaxLength(11, ErrorMessage = "TC Kimlik 11 Karakterden fazla olamaz")]
        [MinLength(11, ErrorMessage = "TC Kimlik 11 Karakterden az olamaz")]
        public string TCKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı boş geçilemez!")]
        public string BasvuruAdiSoyadi { get; set; }
        [Required(ErrorMessage = "Cep Telefonu alanı boş geçilemez!")]
        public string CepTelefonu { get; set; }
        [Required(ErrorMessage = "Email Adresi alanı boş geçilemez!")]
        public string MailAdresi { get; set; }
        [Required(ErrorMessage = "Meslek Seçim alanı boş geçilemez!")]
        public int MeslekID { get; set; }
        [Required(ErrorMessage = "Çalıştığı Kurum alanı boş geçilemez!")]
        public string CalistigiKurum { get; set; }
        [Required(ErrorMessage = "Adres alanı boş geçilemez!")]
        public string Adresi { get; set; }
        public string Mesaj { get; set; }
        [Required(ErrorMessage = "Kurs Başvuru formunu yüklemediniz!")]
        public string Basvuru_Formu { get; set; }
        [Required(ErrorMessage = "Dekont yüklemediniz!")]
        public string Dekont { get; set; }
        [Required(ErrorMessage = "İstenilen diğer belgeleri yüklemediniz!")]
        public string Diger_Belgeler { get; set; }

        [Required(ErrorMessage = "Taahhüt Bilgilerini Onaylamadınız!")]
        public bool TaahhutOnay { get; set; }        
        public DateTime BasvuruTarihi { get; set; }
        public string Guid { get; set; }
        public bool Onay { get; set; }
        public bool Durum { get; set; } // O devam ediyor 1 tamamlandı
        public virtual KaysemMeslek  Meslek { get; set; }
        public virtual KaysemKurs Kurs { get; set; }
    }
}
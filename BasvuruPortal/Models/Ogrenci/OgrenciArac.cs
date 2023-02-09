using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("PersonelArac")]
    public class OgrenciArac
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı boş geçilemez")]
        public string AdiSoyadi { get; set; }

        [Required(ErrorMessage = "TC Kimlik alanı boş geçilemez")]
        [MaxLength(11, ErrorMessage = "TC Kimlik 11 Karakterden fazla olamaz")]
        [MinLength(11, ErrorMessage = "TC Kimlik 11 Karakterden az olamaz")]
        public string TCKimlikNo { get; set; }


        [Required(ErrorMessage = "Sicil No alanı boş geçilemez")]
        public string OgrenciNo { get; set; }

        public string Uyruğu { get; set; }

        public byte birimi { get; set; }

        public string CepTelefonu { get; set; }

        [Required(ErrorMessage = "Plaka alanı boş geçilemez")]
        public string Plaka { get; set; }

        //[Required(ErrorMessage = "Araç Markası alanı boş geçilemez")]
        //public string AracMarka { get; set; }

        [Required(ErrorMessage = "Araç Modeli alanı boş geçilemez")]
        public string AracModeli { get; set; }

        public string ModelYili { get; set; }

        public string AracRengi { get; set; }

        public string RuhsatSahibi { get; set; }

        public string RuhsatSahibiYakinlik { get; set; }

        public DateTime BasvuruTarihi { get; set; }

        public DateTime OnayTarihi { get; set; }

        public string OnaylanayanKisi { get; set; }

        public virtual Statu statu { get; set; }
    }
}
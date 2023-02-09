using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Ogrenci
{
    [Table("TekDersBasvuru")]
    public class TekDersBasvuru
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> DonemId { get; set; } //Tek ders dönemi
        public Nullable<int> FakulteKodu { get; set; }
        public Nullable<int> BolumKodu { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string OgrenciNo { get; set; }
        public string TCKimlikNo { get; set; }
        public string CepTel { get; set; }
        public string Email { get; set; }
        public  Nullable<DateTime> DersAlmaZamani { get; set; }
        public bool DersSecim { get; set; } //1 onay verenler 0 işlem yapmayanlar


    }
}
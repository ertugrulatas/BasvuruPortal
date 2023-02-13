using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("TekDersBasvuru")]
    public class TekDersBasvuru
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> TekDersDonemId { get; set; } //Tek ders dönemi
        public Nullable<int> FakulteKodu { get; set; }
        public string FakulteAdi { get; set; }
        public Nullable<int> BolumKodu { get; set; }
        public string BolumAdi { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public string OgrenciNo { get; set; }
        public string OgrenciTCKNo { get; set; }
        public string OgrenciAdi { get; set; }
        public string OgrenciSoyadi { get; set; }       
        public string OgrenciTelefon { get; set; }
        public string OgrenciEmail { get; set; }
        public  Nullable<DateTime> DersAlmaZamani { get; set; }
        public bool DersSecim { get; set; } //1 onay verenler 0 işlem yapmayanlar
        public string EgitmenSicilNo { get; set; }
        public string EgitmenAdiSoyadi { get; set; }
        public string EgitmenTelefon { get; set; }

        public virtual TekDersDonemi TekDersDonem { get; set; }
    }
}
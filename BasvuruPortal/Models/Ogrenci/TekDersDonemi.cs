using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("TekDersDonemi")]
    public class TekDersDonemi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TekdersYili { get; set; }//2023
        public byte TekdersDonemNo { get; set; } //1 2 3 gibi
        public string TekDersDonemAdi { get; set; }

        public Nullable<DateTime> SinavTarihi { get; set; }
        public Nullable<DateTime> BaslangicTarihi { get; set; }
        public Nullable<DateTime> BitisTarihi { get; set; }
        public bool Durum { get; set; }

        public ICollection<TekDersBasvuru> TekDersBasvurus = new HashSet<TekDersBasvuru>();

    }
}
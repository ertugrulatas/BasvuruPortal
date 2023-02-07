using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Yarismalar
{
    [Table("YarismaBasvuru")]
    public class YarismaBasvuru
    {
        [Key]
        public Guid Id { get; set; }

        public int YarismaId { get; set; }

        public string YarismaciAdi { get; set; }
        public string YarismaciSoyadi { get; set; }

        public string Adres { get; set; }
        public DateTime YarismaciDogumTarihi { get; set; }
        public string YarismaciEmal { get; set; }
        public string YarismaciTelefon { get; set; }

        public string YarismaciPrjeAciklama { get; set; }
        public bool Yarismacibeyan { get; set; }
        public DateTime BasvuruTarihi { get; set; }

        public string YarismaDosyaAdi { get; set; }
        public string YarismaDosyaYolu { get; set; }
        public string YarismaDosyaTuru { get; set; }

        public virtual Yarisma Yarisma { get; set; }
       
    }
}
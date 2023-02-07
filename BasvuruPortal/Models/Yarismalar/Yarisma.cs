using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Yarismalar
{
    [Table("Yarisma")]
    public class Yarisma
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string YarismaAdi { get; set; }
        public string Duzenleyen { get; set; }
        public DateTime BaslangicTarihi {get;set;}

        public DateTime BitisTarihi { get; set; }

        public Nullable<DateTime> SonucTarihi { get; set; }
        public bool Durum { get; set; }

        public int YarismaTurId { get; set; }


        public virtual YarismaTur YarismaTur { get; set; }
        public ICollection<YarismaBasvuru> YarismaBasvurus { get; set; }

        
    }
}
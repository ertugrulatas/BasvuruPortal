using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Yarismalar
{
    [Table("YarismaTur")]
    public class YarismaTur
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string YarismaTuru { get; set; }

        public ICollection<Yarisma> Yarismas { get; set; }

    }
}
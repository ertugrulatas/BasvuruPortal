using BasvuruPortal.Models.isguvenlik;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Genel
{
    [Table("Birimler")]
    public class Birimler
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string BirimAdi { get; set; }

        public byte paretnID { get; set; } //1 Akademik  2-İdari

        public ICollection<isgBildirimler> isgBildirimlers { get; set; } = new HashSet<isgBildirimler>();

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Kaysem
{
    [Table("KaysemKurs")]
    public class KaysemKurs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Kurs_Adi { get; set; }
        public string Aciklama { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool Durum { get; set; }

        public ICollection<KaysemBasvuru> kaysemBasvurus = new HashSet<KaysemBasvuru>();
    }
}
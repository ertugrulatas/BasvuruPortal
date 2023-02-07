using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.Kaysem
{
    [Table("KaysemMeslek")]
    
    public class KaysemMeslek
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Meslek_Adi { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool Durum { get; set; }

        public ICollection<KaysemBasvuru> kaysemBasvurus = new HashSet<KaysemBasvuru>();
    }
}
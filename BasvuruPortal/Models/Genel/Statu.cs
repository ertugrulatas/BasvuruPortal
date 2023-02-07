using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    //1-Akademik 2-Memur 3-Sözleşmeli 4-İşçi 5-35.Madde - 6-Öğrenci 7-Diğer
    [Table("Statu")] 
    public class Statu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string status { get; set; }

        public ICollection<PersonelEmail> personelEmails = new HashSet<PersonelEmail>();
        public ICollection<PersonelArac> personelAracs = new HashSet<PersonelArac>();
    }
}
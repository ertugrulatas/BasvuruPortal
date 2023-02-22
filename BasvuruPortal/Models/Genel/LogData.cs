using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models
{
    [Table("LogData")]
    public class LogData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string PersonelSicilNo { get; set; }
        public string OgrenciNo { get; set; }
        public string IPAdress { get; set; }
        public string Yapilanislem { get; set; }
        public byte CRUD { get; set; } //1 Create 2 Select 3 Update 4 Delete

        public DateTime islemTarihi { get; set; }
    }
}
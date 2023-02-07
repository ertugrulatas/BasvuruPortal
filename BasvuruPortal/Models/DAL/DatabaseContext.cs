using BasvuruPortal.Models.Genel;
using BasvuruPortal.Models.isguvenlik;
using BasvuruPortal.Models.Kaysem;
using BasvuruPortal.Models.Yarismalar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BasvuruPortal.Models.DAL
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Statu> status { get; set; }
        public DbSet<Birimler> birimlers { get; set; }
        public DbSet<PersonelEmail> personelEmails { get; set; }
        public DbSet<PersonelArac> personelAracs { get; set; }

        public DbSet<user> users { get; set; }

        public DbSet<TalepOneri> talepOneris { get; set; }
        public DbSet<HataUsulYolsuzluk> hataUsulYolsuzluks { get; set; }

        public DbSet<Yarisma> Yarismas { get; set; }
        public DbSet<YarismaBasvuru> YarismaBasvurus {get;set;}

        public DbSet<isgBildirimler> isgBildirimlers { get; set; }
        public DbSet<isgBildirimTanim> isgBildirimTanims { get; set; }

        public DbSet<KaysemBasvuru> kaysemBasvurus { get; set; }
        public DbSet<KaysemKurs> kaysemKurss { get; set; }
        public DbSet<KaysemMeslek> kaysemMesleks { get; set; }

       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DatabaseContext()
        {
            InitializeDatabase();
            //Database.SetInitializer(new Myinitiliazer());
            //Database.SetInitializer<DatabaseContext>(null); //migration sonrası çalışması için
        }

        private void InitializeDatabase()
        {
            Database.SetInitializer(new Myinital());
            if (!Database.Exists())
            {
                Database.Initialize(true);
            }
        }

    }
}
namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isgbildirim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.isgBildirimler",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BildirimTanimId = c.Int(nullable: false),
                    AdSoyad = c.String(nullable: false, maxLength: 100),
                    Email = c.String(),
                    Telefon = c.String(),
                    BirimId = c.Int(nullable: false),
                    DigerBirim = c.String(maxLength: 150),
                    Tarih = c.DateTime(nullable: false),
                    Saat = c.String(),
                    Aciklama = c.String(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    DosyaAdi = c.String(),
                    DosyaUrl = c.String(),
                    DosyaTuru = c.String(),
                    MailSent = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.isgBildirimTanim", t => t.BildirimTanimId)
                //.ForeignKey("dbo.Birimler", t => t.BirimId)
                .Index(t => t.BildirimTanimId);
                //.Index(t => t.BirimId);
            
            CreateTable(
                "dbo.isgBildirimTanim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BildirimAdi = c.String(),
                        MailSentAdress = c.String(),
                        Aciklama = c.String(),
                        Durum = c.Boolean(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.isgBildirimler", "BirimId", "dbo.Birimler");
            DropForeignKey("dbo.isgBildirimler", "BildirimTanimId", "dbo.isgBildirimTanim");
            DropIndex("dbo.isgBildirimler", new[] { "BirimId" });
            DropIndex("dbo.isgBildirimler", new[] { "BildirimTanimId" });
            DropTable("dbo.isgBildirimTanim");
            DropTable("dbo.isgBildirimler");
        }
    }
}

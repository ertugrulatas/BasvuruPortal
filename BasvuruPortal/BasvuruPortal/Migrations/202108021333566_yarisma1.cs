namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yarisma1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YarismaBasvuru",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        YarismaId = c.Int(nullable: false),
                        YarismaciAdi = c.String(),
                        YarismaciSoyadi = c.String(),
                        Adres = c.String(),
                        YarismaciDogumTarihi = c.DateTime(nullable: false),
                        YarismaciEmal = c.String(),
                        YarismaciTelefon = c.String(),
                        YarismaciPrjeAciklama = c.String(),
                        Yarismacibeyan = c.Boolean(nullable: false),
                        BasvuruTarihi = c.DateTime(nullable: false),
                        YarismaDosyaAdi = c.String(),
                        YarismaDosyaYolu = c.String(),
                        YarismaDosyaTuru = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Yarisma", t => t.YarismaId)
                .Index(t => t.YarismaId);
            
            CreateTable(
                "dbo.Yarisma",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YarismaAdi = c.String(),
                        Duzenleyen = c.String(),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(nullable: false),
                        SonucTarihi = c.DateTime(),
                        Durum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YarismaBasvuru", "YarismaId", "dbo.Yarisma");
            DropIndex("dbo.YarismaBasvuru", new[] { "YarismaId" });
            DropTable("dbo.Yarisma");
            DropTable("dbo.YarismaBasvuru");
        }
    }
}

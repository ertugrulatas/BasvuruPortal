namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tekders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TekDersBasvuru",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TekDersDonemId = c.Int(),
                        FakulteKodu = c.Int(),
                        FakulteAdi = c.String(),
                        BolumKodu = c.Int(),
                        BolumAdi = c.String(),
                        DersKodu = c.String(),
                        DersAdi = c.String(),
                        OgrenciNo = c.String(),
                        OgrenciTCKNo = c.String(),
                        OgrenciAdi = c.String(),
                        OgrenciSoyadi = c.String(),
                        OgrenciTelefon = c.String(),
                        OgrenciEmail = c.String(),
                        DersAlmaZamani = c.DateTime(),
                        DersSecim = c.Boolean(nullable: false),
                        EgitmenSicilNo = c.String(),
                        EgitmenAdiSoyadi = c.String(),
                        EgitmenTelefon = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TekDersDonemi", t => t.TekDersDonemId)
                .Index(t => t.TekDersDonemId);
            
            CreateTable(
                "dbo.TekDersDonemi",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TekdersYili = c.Int(nullable: false),
                        TekdersDonemNo = c.Byte(nullable: false),
                        TekDersDonemAdi = c.String(),
                        BaslangicTarihi = c.DateTime(),
                        BitisTarihi = c.DateTime(),
                        Durum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TekDersBasvuru", "TekDersDonemId", "dbo.TekDersDonemi");
            DropIndex("dbo.TekDersBasvuru", new[] { "TekDersDonemId" });
            DropTable("dbo.TekDersDonemi");
            DropTable("dbo.TekDersBasvuru");
        }
    }
}

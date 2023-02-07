namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24032021_taleptableadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TalepOneri",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tip = c.String(nullable: false),
                        Konu = c.String(maxLength: 100),
                        Mesaj = c.String(),
                        BasvuruAdiSoyadi = c.String(),
                        Telefon = c.String(),
                        MailAdresi = c.String(),
                        BasvuruTarihi = c.DateTime(nullable: false),
                        Adresi = c.String(),
                        Durum = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TalepOneri");
        }
    }
}

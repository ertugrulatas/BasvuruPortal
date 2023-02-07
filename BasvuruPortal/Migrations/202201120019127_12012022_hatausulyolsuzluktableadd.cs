namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12012022_hatausulyolsuzluktableadd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
  "dbo.HataUsulYolsuzluk",
  c => new
  {
      ID = c.Int(nullable: false, identity: true),
      Tip = c.String(nullable: false),
      Konu = c.String(maxLength: 100),
      Mesaj = c.String(),
      BasvuruAdiSoyadi = c.String(),
      TCKimlikNo = c.String(),
      GorevYeri = c.String(),
      CepTelefonu = c.String(),
      MailAdresi = c.String(),
      BildirimTarihi = c.DateTime(nullable: false),
      Adresi = c.String(),
      IsTelefonu = c.String(),
      Durum = c.Boolean(nullable: false),
  })
  .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.HataUsulYolsuzluk");
        }
    }
}

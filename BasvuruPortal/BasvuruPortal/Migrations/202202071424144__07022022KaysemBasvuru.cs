namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07022022KaysemBasvuru : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KaysemBasvuru", "BasvuruAdiSoyadi", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "CepTelefonu", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "MailAdresi", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "CalistigiKurum", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "Adresi", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "Basvuru_Formu", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "Dekont", c => c.String(nullable: false));
            AlterColumn("dbo.KaysemBasvuru", "Diger_Belgeler", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KaysemBasvuru", "Diger_Belgeler", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "Dekont", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "Basvuru_Formu", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "Adresi", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "CalistigiKurum", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "MailAdresi", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "CepTelefonu", c => c.String());
            AlterColumn("dbo.KaysemBasvuru", "BasvuruAdiSoyadi", c => c.String());
        }
    }
}

namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03022022KursBasvuruEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KaysemBasvuru", "TCKimlikNo", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.KaysemBasvuru", "Basvuru_Formu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KaysemBasvuru", "Basvuru_Formu");
            DropColumn("dbo.KaysemBasvuru", "TCKimlikNo");
        }
    }
}

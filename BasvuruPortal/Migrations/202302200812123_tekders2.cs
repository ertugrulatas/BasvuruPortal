namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tekders2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KaysemBasvuru", "TaahhutOnay", c => c.Boolean(nullable: false));
            AddColumn("dbo.TekDersBasvuru", "OgrenciAdres", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TekDersBasvuru", "OgrenciAdres");
            DropColumn("dbo.KaysemBasvuru", "TaahhutOnay");
        }
    }
}

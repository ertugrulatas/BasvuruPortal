namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class araconay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonelArac", "onay", c => c.Byte(nullable: false));
            AlterColumn("dbo.PersonelEmail", "onay", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonelEmail", "onay", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PersonelArac", "onay", c => c.Boolean(nullable: false));
        }
    }
}

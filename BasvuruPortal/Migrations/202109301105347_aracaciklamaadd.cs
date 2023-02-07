namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aracaciklamaadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonelArac", "Aciklama", c => c.String());
            AddColumn("dbo.PersonelEmail", "Aciklama", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PersonelEmail", "Aciklama");
            DropColumn("dbo.PersonelArac", "Aciklama");
        }
    }
}

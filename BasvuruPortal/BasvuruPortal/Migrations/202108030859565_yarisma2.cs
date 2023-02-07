namespace BasvuruPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yarisma2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YarismaTur",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YarismaTuru = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Yarisma", "YarismaTurId", c => c.Int(nullable: false));
            CreateIndex("dbo.Yarisma", "YarismaTurId");
            AddForeignKey("dbo.Yarisma", "YarismaTurId", "dbo.YarismaTur", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yarisma", "YarismaTurId", "dbo.YarismaTur");
            DropIndex("dbo.Yarisma", new[] { "YarismaTurId" });
            DropColumn("dbo.Yarisma", "YarismaTurId");
            DropTable("dbo.YarismaTur");
        }
    }
}

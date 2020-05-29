namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update040120 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            AddColumn("dbo.Users", "RoleID", c => c.Int());
            CreateIndex("dbo.Users", "RoleID");
            AddForeignKey("dbo.Users", "RoleID", "dbo.Roles", "RoleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropColumn("dbo.Users", "RoleID");
            DropTable("dbo.Roles");
        }
    }
}

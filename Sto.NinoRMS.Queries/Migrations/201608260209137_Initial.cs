namespace OERS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPermissions",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        PermissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.PermissionID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PermissionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermissions", "PermissionID", "dbo.Permissions");
            DropForeignKey("dbo.UserPermissions", "UserID", "dbo.Users");
            DropIndex("dbo.UserPermissions", new[] { "PermissionID" });
            DropIndex("dbo.UserPermissions", new[] { "UserID" });
            DropTable("dbo.UserPermissions");
        }
    }
}

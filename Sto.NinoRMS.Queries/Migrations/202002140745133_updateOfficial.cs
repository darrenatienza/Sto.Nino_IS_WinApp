namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOfficial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OfficialPositions", "Title", c => c.String());
            AddColumn("dbo.Officials", "ResidentID", c => c.Int(nullable: false));
            AddColumn("dbo.Officials", "OfficialPositionID", c => c.Int(nullable: false));
            AddColumn("dbo.Officials", "Designation", c => c.String());
            CreateIndex("dbo.Officials", "ResidentID");
            CreateIndex("dbo.Officials", "OfficialPositionID");
            AddForeignKey("dbo.Officials", "OfficialPositionID", "dbo.OfficialPositions", "OfficialPositionID", cascadeDelete: true);
            AddForeignKey("dbo.Officials", "ResidentID", "dbo.Residents", "ResidentID", cascadeDelete: true);
            DropColumn("dbo.OfficialPositions", "Name");
            DropColumn("dbo.Officials", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Officials", "Name", c => c.String());
            AddColumn("dbo.OfficialPositions", "Name", c => c.String());
            DropForeignKey("dbo.Officials", "ResidentID", "dbo.Residents");
            DropForeignKey("dbo.Officials", "OfficialPositionID", "dbo.OfficialPositions");
            DropIndex("dbo.Officials", new[] { "OfficialPositionID" });
            DropIndex("dbo.Officials", new[] { "ResidentID" });
            DropColumn("dbo.Officials", "Designation");
            DropColumn("dbo.Officials", "OfficialPositionID");
            DropColumn("dbo.Officials", "ResidentID");
            DropColumn("dbo.OfficialPositions", "Title");
        }
    }
}

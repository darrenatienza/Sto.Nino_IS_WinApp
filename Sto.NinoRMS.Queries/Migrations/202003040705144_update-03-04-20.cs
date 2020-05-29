namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update030420 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accomplishments",
                c => new
                    {
                        AccomplishmentID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.AccomplishmentID);
            
            CreateTable(
                "dbo.CommonHealthProfiles",
                c => new
                    {
                        CommonHealthProfileID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.CommonHealthProfileID);
            
            CreateTable(
                "dbo.HealthDataBoards",
                c => new
                    {
                        HealthDataBoardID = c.Int(nullable: false, identity: true),
                        CommonHealthProfileID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HealthDataBoardID)
                .ForeignKey("dbo.CommonHealthProfiles", t => t.CommonHealthProfileID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CommonHealthProfileID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.QuarterlyReports",
                c => new
                    {
                        QuarterlyReportID = c.Int(nullable: false, identity: true),
                        AccomplishmentID = c.Int(nullable: false),
                        Quarter = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuarterlyReportID)
                .ForeignKey("dbo.Accomplishments", t => t.AccomplishmentID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.AccomplishmentID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuarterlyReports", "UserID", "dbo.Users");
            DropForeignKey("dbo.QuarterlyReports", "AccomplishmentID", "dbo.Accomplishments");
            DropForeignKey("dbo.HealthDataBoards", "UserID", "dbo.Users");
            DropForeignKey("dbo.HealthDataBoards", "CommonHealthProfileID", "dbo.CommonHealthProfiles");
            DropIndex("dbo.QuarterlyReports", new[] { "UserID" });
            DropIndex("dbo.QuarterlyReports", new[] { "AccomplishmentID" });
            DropIndex("dbo.HealthDataBoards", new[] { "UserID" });
            DropIndex("dbo.HealthDataBoards", new[] { "CommonHealthProfileID" });
            DropTable("dbo.QuarterlyReports");
            DropTable("dbo.HealthDataBoards");
            DropTable("dbo.CommonHealthProfiles");
            DropTable("dbo.Accomplishments");
        }
    }
}

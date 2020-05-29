namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BrgyBizClearances",
                c => new
                    {
                        BrgyBizClearanceID = c.Int(nullable: false, identity: true),
                        ResidentID = c.Int(nullable: false),
                        BizName = c.String(),
                        isFollowingAllProvision = c.Boolean(nullable: false),
                        isHinderPermitApplication = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BrgyBizClearanceID)
                .ForeignKey("dbo.Residents", t => t.ResidentID, cascadeDelete: true)
                .Index(t => t.ResidentID);
            
            CreateTable(
                "dbo.Residents",
                c => new
                    {
                        ResidentID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Birthday = c.String(),
                        Purok = c.String(),
                        Sitio = c.String(),
                        ContactNumber = c.String(),
                        Occupation = c.String(),
                    })
                .PrimaryKey(t => t.ResidentID);
            
            CreateTable(
                "dbo.BrgyClearances",
                c => new
                    {
                        BrgyClearanceID = c.Int(nullable: false, identity: true),
                        ResidentID = c.Int(nullable: false),
                        PurposeOfRequest = c.String(),
                        CommTaxtCert = c.String(),
                        DateIssued = c.DateTime(nullable: false),
                        PlaceIssued = c.String(),
                        TinNo = c.String(),
                    })
                .PrimaryKey(t => t.BrgyClearanceID)
                .ForeignKey("dbo.Residents", t => t.ResidentID, cascadeDelete: true)
                .Index(t => t.ResidentID);
            
            CreateTable(
                "dbo.Indigencies",
                c => new
                    {
                        IndigencyID = c.Int(nullable: false, identity: true),
                        ResidentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IndigencyID)
                .ForeignKey("dbo.Residents", t => t.ResidentID, cascadeDelete: true)
                .Index(t => t.ResidentID);
            
            CreateTable(
                "dbo.OfficialPositions",
                c => new
                    {
                        OfficialPositionID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OfficialPositionID);
            
            CreateTable(
                "dbo.Officials",
                c => new
                    {
                        OfficialID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OfficialID);
            
            CreateTable(
                "dbo.Residencies",
                c => new
                    {
                        ResidencyID = c.Int(nullable: false, identity: true),
                        ResidentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResidencyID)
                .ForeignKey("dbo.Residents", t => t.ResidentID, cascadeDelete: true)
                .Index(t => t.ResidentID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 250),
                        MiddleName = c.String(nullable: false, maxLength: 250),
                        LastName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Residencies", "ResidentID", "dbo.Residents");
            DropForeignKey("dbo.Indigencies", "ResidentID", "dbo.Residents");
            DropForeignKey("dbo.BrgyClearances", "ResidentID", "dbo.Residents");
            DropForeignKey("dbo.BrgyBizClearances", "ResidentID", "dbo.Residents");
            DropIndex("dbo.Residencies", new[] { "ResidentID" });
            DropIndex("dbo.Indigencies", new[] { "ResidentID" });
            DropIndex("dbo.BrgyClearances", new[] { "ResidentID" });
            DropIndex("dbo.BrgyBizClearances", new[] { "ResidentID" });
            DropTable("dbo.Users");
            DropTable("dbo.Residencies");
            DropTable("dbo.Officials");
            DropTable("dbo.OfficialPositions");
            DropTable("dbo.Indigencies");
            DropTable("dbo.BrgyClearances");
            DropTable("dbo.Residents");
            DropTable("dbo.BrgyBizClearances");
        }
    }
}

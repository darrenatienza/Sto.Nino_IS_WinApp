namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update040220 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        ChildrenID = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        Occupation = c.String(),
                        ResidentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChildrenID)
                .ForeignKey("dbo.Residents", t => t.ResidentID, cascadeDelete: true)
                .Index(t => t.ResidentID);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EducationID = c.Int(nullable: false, identity: true),
                        SchoolName = c.String(),
                        Level = c.String(),
                        InclusiveDate = c.String(),
                        Course = c.String(),
                        ResidentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EducationID)
                .ForeignKey("dbo.Residents", t => t.ResidentID, cascadeDelete: true)
                .Index(t => t.ResidentID);
            
            AddColumn("dbo.Residents", "FirstName", c => c.String());
            AddColumn("dbo.Residents", "MiddleName", c => c.String());
            AddColumn("dbo.Residents", "LastName", c => c.String());
            AddColumn("dbo.Residents", "Gender", c => c.String());
            AddColumn("dbo.Residents", "Religion", c => c.String());
            AddColumn("dbo.Residents", "FathersFullName", c => c.String());
            AddColumn("dbo.Residents", "FathersOccupation", c => c.String());
            AddColumn("dbo.Residents", "MothersFullName", c => c.String());
            AddColumn("dbo.Residents", "MothersOccupation", c => c.String());
            AddColumn("dbo.Residents", "CivilStatus", c => c.String());
            AddColumn("dbo.Residents", "Citizenship", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Educations", "ResidentID", "dbo.Residents");
            DropForeignKey("dbo.Children", "ResidentID", "dbo.Residents");
            DropIndex("dbo.Educations", new[] { "ResidentID" });
            DropIndex("dbo.Children", new[] { "ResidentID" });
            DropColumn("dbo.Residents", "Citizenship");
            DropColumn("dbo.Residents", "CivilStatus");
            DropColumn("dbo.Residents", "MothersOccupation");
            DropColumn("dbo.Residents", "MothersFullName");
            DropColumn("dbo.Residents", "FathersOccupation");
            DropColumn("dbo.Residents", "FathersFullName");
            DropColumn("dbo.Residents", "Religion");
            DropColumn("dbo.Residents", "Gender");
            DropColumn("dbo.Residents", "LastName");
            DropColumn("dbo.Residents", "MiddleName");
            DropColumn("dbo.Residents", "FirstName");
            DropTable("dbo.Educations");
            DropTable("dbo.Children");
        }
    }
}

namespace OERS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_College : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colleges",
                c => new
                    {
                        CollegeID = c.Int(nullable: false, identity: true),
                        CollegeCode = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CollegeID);
            
            AddColumn("dbo.Employees", "CollegeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "CollegeID");
            AddForeignKey("dbo.Employees", "CollegeID", "dbo.Colleges", "CollegeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "CollegeID", "dbo.Colleges");
            DropIndex("dbo.Employees", new[] { "CollegeID" });
            DropColumn("dbo.Employees", "CollegeID");
            DropTable("dbo.Colleges");
        }
    }
}

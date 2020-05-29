namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update030420_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuarterlyReports", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuarterlyReports", "Count");
        }
    }
}

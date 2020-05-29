namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update030420_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuarterlyReports", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuarterlyReports", "Gender");
        }
    }
}

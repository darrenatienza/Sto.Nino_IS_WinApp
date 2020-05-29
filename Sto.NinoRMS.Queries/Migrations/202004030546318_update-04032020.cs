namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update04032020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Children", "FullName");
        }
    }
}

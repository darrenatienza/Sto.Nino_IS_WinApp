namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update040320 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Residents", "SpouseFullName", c => c.String());
            AddColumn("dbo.Residents", "SpouseOccupation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Residents", "SpouseOccupation");
            DropColumn("dbo.Residents", "SpouseFullName");
        }
    }
}

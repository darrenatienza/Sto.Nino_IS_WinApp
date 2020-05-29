namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateResidency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Residencies", "LengthOfResidency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Residencies", "LengthOfResidency");
        }
    }
}

namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update032620 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Residents", "ImageName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Residents", "ImageName");
        }
    }
}

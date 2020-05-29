namespace Sto.NinoRMS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyDateOnResident : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Residents", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Residents", "Birthday", c => c.String());
        }
    }
}

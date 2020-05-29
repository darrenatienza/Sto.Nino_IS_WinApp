namespace OERS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_Visible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "IsVisible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "IsVisible");
        }
    }
}

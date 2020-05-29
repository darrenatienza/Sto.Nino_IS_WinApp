namespace OERS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee_AliasName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "AliasName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "AliasName");
        }
    }
}

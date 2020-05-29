namespace OERS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationLogs",
                c => new
                    {
                        ReservationLogID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        ItemDescription = c.String(),
                        DateTimeLog = c.DateTime(nullable: false),
                        Room = c.String(),
                        ReservationStatus = c.String(),
                    })
                .PrimaryKey(t => t.ReservationLogID);
            
            AddColumn("dbo.Items", "IsVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Items", "Notes", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Items", "Notes");
            DropColumn("dbo.Items", "IsVisible");
            DropTable("dbo.ReservationLogs");
        }
    }
}

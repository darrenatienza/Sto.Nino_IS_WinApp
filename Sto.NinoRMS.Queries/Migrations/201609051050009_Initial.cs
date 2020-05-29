namespace OERS.Queries.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colleges",
                c => new
                    {
                        CollegeID = c.Int(nullable: false, identity: true),
                        CollegeCode = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CollegeID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        AliasName = c.String(),
                        CollegeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Colleges", t => t.CollegeID, cascadeDelete: true)
                .Index(t => t.CollegeID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        ReservationDate = c.DateTime(nullable: false),
                        ReservationStatus = c.String(),
                        ItemID = c.Int(nullable: false),
                        TimeRangeID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .ForeignKey("dbo.TimeRanges", t => t.TimeRangeID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.TimeRangeID)
                .Index(t => t.EmployeeID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoomID);
            
            CreateTable(
                "dbo.TimeRanges",
                c => new
                    {
                        TimeRangeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TimeRangeID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PermissionID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false),
                        PasswordSalt = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.UserPermissions",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        PermissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.PermissionID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PermissionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPermissions", "PermissionID", "dbo.Permissions");
            DropForeignKey("dbo.UserPermissions", "UserID", "dbo.Users");
            DropForeignKey("dbo.Reservations", "TimeRangeID", "dbo.TimeRanges");
            DropForeignKey("dbo.Reservations", "RoomID", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Reservations", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CollegeID", "dbo.Colleges");
            DropIndex("dbo.UserPermissions", new[] { "PermissionID" });
            DropIndex("dbo.UserPermissions", new[] { "UserID" });
            DropIndex("dbo.Reservations", new[] { "RoomID" });
            DropIndex("dbo.Reservations", new[] { "EmployeeID" });
            DropIndex("dbo.Reservations", new[] { "TimeRangeID" });
            DropIndex("dbo.Reservations", new[] { "ItemID" });
            DropIndex("dbo.Employees", new[] { "CollegeID" });
            DropTable("dbo.UserPermissions");
            DropTable("dbo.Users");
            DropTable("dbo.Permissions");
            DropTable("dbo.TimeRanges");
            DropTable("dbo.Rooms");
            DropTable("dbo.Items");
            DropTable("dbo.Reservations");
            DropTable("dbo.Employees");
            DropTable("dbo.Colleges");
        }
    }
}

namespace ChattingBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Event_Id = c.Int(nullable: false),
                        By_Who = c.String(),
                        Text = c.String(nullable: false, maxLength: 1000),
                        Mark = c.Boolean(nullable: false),
                        Event_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id1)
                .Index(t => t.Event_Id1);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        StartBy_Who = c.String(),
                        Public_Event = c.Boolean(nullable: false),
                        Secret_Code = c.String(),
                        Event_Status = c.Boolean(nullable: false),
                        Event_Name = c.String(nullable: false, maxLength: 100),
                        Picture = c.String(maxLength: 50),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Event_Id1", "dbo.Events");
            DropIndex("dbo.Comments", new[] { "Event_Id1" });
            DropTable("dbo.Events");
            DropTable("dbo.Comments");
        }
    }
}

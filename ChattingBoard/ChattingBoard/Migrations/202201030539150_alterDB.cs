namespace ChattingBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterDB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "Event_Id1" });
            DropColumn("dbo.Comments", "Event_Id");
            RenameColumn(table: "dbo.Comments", name: "Event_Id1", newName: "Event_Id");
            AlterColumn("dbo.Comments", "Event_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Event_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            AlterColumn("dbo.Comments", "Event_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Comments", name: "Event_Id", newName: "Event_Id1");
            AddColumn("dbo.Comments", "Event_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Event_Id1");
        }
    }
}

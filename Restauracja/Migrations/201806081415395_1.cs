namespace Restauracja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Orders", new[] { "Waiter_Id" });
            DropColumn("dbo.Orders", "WaiterId");
            RenameColumn(table: "dbo.Orders", name: "Waiter_Id", newName: "WaiterId");
            AlterColumn("dbo.Orders", "WaiterId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "WaiterId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "WaiterId" });
            AlterColumn("dbo.Orders", "WaiterId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "WaiterId", newName: "Waiter_Id");
            AddColumn("dbo.Orders", "WaiterId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Waiter_Id");
        }
    }
}

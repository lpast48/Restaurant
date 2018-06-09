namespace Restauracja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "Visibility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Order_Meal", "IssueTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order_Meal", "IssueTime");
            DropColumn("dbo.Meals", "Visibility");
        }
    }
}

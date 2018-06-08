namespace Restauracja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "MealTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "MealTime", c => c.DateTime(nullable: false));
        }
    }
}

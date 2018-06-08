namespace Restauracja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Meals", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Meals", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Meals", "Ingredients", c => c.String(nullable: false));
            AlterColumn("dbo.Meals", "Allergens", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Meals", "Allergens", c => c.String());
            AlterColumn("dbo.Meals", "Ingredients", c => c.String());
            AlterColumn("dbo.Meals", "Description", c => c.String());
            AlterColumn("dbo.Meals", "Name", c => c.String());
        }
    }
}

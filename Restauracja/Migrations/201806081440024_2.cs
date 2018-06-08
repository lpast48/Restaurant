namespace Restauracja.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.Meals", "Allergens", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "Allergens");
            DropColumn("dbo.Meals", "Price");
        }
    }
}

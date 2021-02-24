namespace MyRecipe2.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedEnumForDishType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "TypeOfDish", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipe", "TypeOfDish");
        }
    }
}

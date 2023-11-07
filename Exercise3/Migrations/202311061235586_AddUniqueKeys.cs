namespace Exercise3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueKeys : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Manufacturers", "Name", unique: true, name: "IX_Manufacturers");
            CreateIndex("dbo.Products", "Name", unique: true, name: "IX_Products");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", "IX_Products");
            DropIndex("dbo.Manufacturers", "IX_Manufacturers");
        }
    }
}

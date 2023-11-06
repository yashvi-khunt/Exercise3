namespace Exercise3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyAnnotationsToTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Manufacturers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Rates", "Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rates", "Amount", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Manufacturers", "Name", c => c.String());
        }
    }
}

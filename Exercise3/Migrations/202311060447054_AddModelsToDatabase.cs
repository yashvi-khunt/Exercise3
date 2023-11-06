namespace Exercise3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelsToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ManufacturerId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.PurchaseHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        RateId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.Rates", t => t.RateId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ManufacturerId)
                .Index(t => t.ProductId)
                .Index(t => t.RateId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.String(),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseHistories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PurchaseHistories", "RateId", "dbo.Rates");
            DropForeignKey("dbo.Rates", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseHistories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseHistories", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropIndex("dbo.Rates", new[] { "ProductId" });
            DropIndex("dbo.PurchaseHistories", new[] { "User_Id" });
            DropIndex("dbo.PurchaseHistories", new[] { "RateId" });
            DropIndex("dbo.PurchaseHistories", new[] { "ProductId" });
            DropIndex("dbo.PurchaseHistories", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropTable("dbo.Rates");
            DropTable("dbo.PurchaseHistories");
            DropTable("dbo.Products");
            DropTable("dbo.Manufacturers");
        }
    }
}

namespace Exercise3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyInPurshaceHistory : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PurchaseHistories", name: "User_Id", newName: "AspNetUserId");
            RenameIndex(table: "dbo.PurchaseHistories", name: "IX_User_Id", newName: "IX_AspNetUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PurchaseHistories", name: "IX_AspNetUserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.PurchaseHistories", name: "AspNetUserId", newName: "User_Id");
        }
    }
}

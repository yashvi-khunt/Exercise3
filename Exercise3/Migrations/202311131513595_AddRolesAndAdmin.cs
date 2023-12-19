namespace Exercise3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRolesAndAdmin : DbMigration
    {
        public override void Up()
        {

            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'38e51f95-ec21-4609-9129-caca323779d0', N'guest@abc.com', 0, N'APldFbUH2LsTQZxahbqaiSVXm+hwqO00RiJTxwpjWNMSJmrcvJlLB/qeJgoGoQT5vA==', N'c070e7a9-590a-4b81-8589-e52de9113d7f', NULL, 0, 0, NULL, 1, 0, N'guest@abc.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6b75b5cb-14d5-488b-a0cb-454c43605822', N'admin@abc.com', 0, N'ADzQslFvm3Fp5SDQtMws3Cgc9oYl5IQG6pgPpZ0HXEPlwtHIXFEXcJWWbTRGlB2nzA==', N'59255171-7a84-4b39-9887-864ff3b0726f', NULL, 0, 0, NULL, 1, 0, N'admin@abc.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'feb7726a-43ce-4dfb-bb4d-ff8950f23e4b', N'CanManageCustomers')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId],[RoleId]) Values (N'6b75b5cb-14d5-488b-a0cb-454c43605822',N'feb7726a-43ce-4dfb-bb4d-ff8950f23e4b')

");
        }
        
        public override void Down()
        {
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mgs : DbMigration
    {
        public override void Up()
        {
        //    CreateTable(
        //        "dbo.Roles",
        //        c => new
        //            {
        //                Id = c.String(nullable: false, maxLength: 128),
        //                Name = c.String(nullable: false, maxLength: 256),
        //            })
        //        .PrimaryKey(t => t.Id)
        //        .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        //    CreateTable(
        //        "dbo.UserRoles",
        //        c => new
        //            {
        //                UserId = c.String(nullable: false, maxLength: 128),
        //                RoleId = c.String(nullable: false, maxLength: 128),
        //                IdentityUser_Id = c.String(maxLength: 128),
        //            })
        //        .PrimaryKey(t => new { t.UserId, t.RoleId })
        //        .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
        //        .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
        //        .Index(t => t.RoleId)
        //        .Index(t => t.IdentityUser_Id);
            
        //    CreateTable(
        //        "dbo.Users",
        //        c => new
        //            {
        //                Id = c.String(nullable: false, maxLength: 128),
        //                Email = c.String(),
        //                EmailConfirmed = c.Boolean(nullable: false),
        //                PasswordHash = c.String(),
        //                SecurityStamp = c.String(),
        //                PhoneNumber = c.String(),
        //                PhoneNumberConfirmed = c.Boolean(nullable: false),
        //                TwoFactorEnabled = c.Boolean(nullable: false),
        //                LockoutEndDateUtc = c.DateTime(),
        //                LockoutEnabled = c.Boolean(nullable: false),
        //                AccessFailedCount = c.Int(nullable: false),
        //                UserName = c.String(),
        //                Address = c.String(),
        //                Discriminator = c.String(nullable: false, maxLength: 128),
        //                Carts_CartID = c.Int(),
        //                Wishlists_WishlistID = c.Int(),
        //            })
        //        .PrimaryKey(t => t.Id)
        //        .ForeignKey("dbo.Carts", t => t.Carts_CartID)
        //        .ForeignKey("dbo.Wishlists", t => t.Wishlists_WishlistID)
        //        .Index(t => t.Carts_CartID)
        //        .Index(t => t.Wishlists_WishlistID);
            
        //    CreateTable(
        //        "dbo.Carts",
        //        c => new
        //            {
        //                CartID = c.Int(nullable: false, identity: true),
        //                DateLastModified = c.DateTime(nullable: false),
        //                Status = c.Boolean(nullable: false),
        //            })
        //        .PrimaryKey(t => t.CartID);
            
        //    CreateTable(
        //        "dbo.UserClaims",
        //        c => new
        //            {
        //                Id = c.Int(nullable: false, identity: true),
        //                UserId = c.String(),
        //                ClaimType = c.String(),
        //                ClaimValue = c.String(),
        //                IdentityUser_Id = c.String(maxLength: 128),
        //            })
        //        .PrimaryKey(t => t.Id)
        //        .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
        //        .Index(t => t.IdentityUser_Id);
            
        //    CreateTable(
        //        "dbo.UserLogins",
        //        c => new
        //            {
        //                LoginProvider = c.String(nullable: false, maxLength: 128),
        //                ProviderKey = c.String(nullable: false, maxLength: 128),
        //                UserId = c.String(nullable: false, maxLength: 128),
        //                IdentityUser_Id = c.String(maxLength: 128),
        //            })
        //        .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
        //        .ForeignKey("dbo.Users", t => t.IdentityUser_Id)
        //        .Index(t => t.IdentityUser_Id);
            
        //    CreateTable(
        //        "dbo.Wishlists",
        //        c => new
        //            {
        //                WishlistID = c.Int(nullable: false, identity: true),
        //                Status = c.Boolean(nullable: false),
        //            })
        //        .PrimaryKey(t => t.WishlistID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            //DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users");
            //DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users");
            //DropForeignKey("dbo.Users", "Wishlists_WishlistID", "dbo.Wishlists");
            //DropForeignKey("dbo.Users", "Carts_CartID", "dbo.Carts");
            //DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            //DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            //DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            //DropIndex("dbo.Users", new[] { "Wishlists_WishlistID" });
            //DropIndex("dbo.Users", new[] { "Carts_CartID" });
            //DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            //DropIndex("dbo.UserRoles", new[] { "RoleId" });
            //DropIndex("dbo.Roles", "RoleNameIndex");
            //DropTable("dbo.Wishlists");
            //DropTable("dbo.UserLogins");
            //DropTable("dbo.UserClaims");
            //DropTable("dbo.Carts");
            //DropTable("dbo.Users");
            //DropTable("dbo.UserRoles");
            //DropTable("dbo.Roles");
        }
    }
}

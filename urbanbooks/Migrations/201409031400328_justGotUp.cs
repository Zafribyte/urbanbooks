namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justGotUp : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Products", "AuthorID", "dbo.Authors");
            //DropIndex("dbo.Products", new[] { "AuthorID" });
            //DropTable("dbo.Authors");
            //DropTable("dbo.Products");
            //DropTable("dbo.BookCategories");
            //DropTable("dbo.CartItems");
            //DropTable("dbo.Companies");
            //DropTable("dbo.Employees");
            //DropTable("dbo.FullRegisterViewModels");
            //DropTable("dbo.Orders");
            //DropTable("dbo.Specials");
            //DropTable("dbo.WishlistItems");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.WishlistItems",
            //    c => new
            //        {
            //            WishlistItemID = c.Int(nullable: false, identity: true),
            //            WishlistID = c.Int(nullable: false),
            //            ProductID = c.Int(nullable: false),
            //            DateAdded = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.WishlistItemID);
            
            //CreateTable(
            //    "dbo.Specials",
            //    c => new
            //        {
            //            SpecialID = c.Int(nullable: false, identity: true),
            //            StartDate = c.DateTime(nullable: false),
            //            EndDate = c.DateTime(nullable: false),
            //            Description = c.String(),
            //            CutDownPercentage = c.Int(nullable: false),
            //            SpecialPrice = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SpecialID);
            
            //CreateTable(
            //    "dbo.Orders",
            //    c => new
            //        {
            //            OrderNo = c.Int(nullable: false, identity: true),
            //            DateCreated = c.DateTime(nullable: false),
            //            DateLastModified = c.DateTime(nullable: false),
            //            Status = c.Boolean(nullable: false),
            //            DateSent = c.DateTime(nullable: false),
            //            SupplierID = c.Int(nullable: false),
            //            EmployeeID = c.Int(nullable: false),
            //            InvoiceID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.OrderNo);
            
            //CreateTable(
            //    "dbo.FullRegisterViewModels",
            //    c => new
            //        {
            //            CustomerID = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(maxLength: 150),
            //            LastName = c.String(maxLength: 150),
            //            Address = c.String(maxLength: 150),
            //            CellPhone = c.String(nullable: false),
            //            Email = c.String(nullable: false),
            //            Password = c.String(nullable: false, maxLength: 100),
            //            ConfirmPassword = c.String(),
            //        })
            //    .PrimaryKey(t => t.CustomerID);
            
            //CreateTable(
            //    "dbo.Employees",
            //    c => new
            //        {
            //            EmployeeID = c.Int(nullable: false, identity: true),
            //            User_Id = c.String(),
            //            FirstName = c.String(),
            //            LastName = c.String(),
            //        })
            //    .PrimaryKey(t => t.EmployeeID);
            
            //CreateTable(
            //    "dbo.Companies",
            //    c => new
            //        {
            //            CompanyRegistration = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            TaxRefferenceNumber = c.String(),
            //            VATRegistrationNumber = c.Int(nullable: false),
            //            VATPercentage = c.Double(nullable: false),
            //            BookMarkUp = c.Double(nullable: false),
            //            TechMarkUp = c.Double(nullable: false),
            //            Address = c.String(),
            //            Telephone = c.String(),
            //            Fax = c.String(),
            //            Email = c.String(),
            //        })
            //    .PrimaryKey(t => t.CompanyRegistration);
            
            //CreateTable(
            //    "dbo.CartItems",
            //    c => new
            //        {
            //            CartItemID = c.Int(nullable: false, identity: true),
            //            CartID = c.Int(nullable: false),
            //            ProductID = c.Int(nullable: false),
            //            Quantity = c.Int(nullable: false),
            //            DateAdded = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CartItemID);
            
            //CreateTable(
            //    "dbo.BookCategories",
            //    c => new
            //        {
            //            BookCategoryID = c.Int(nullable: false, identity: true),
            //            CategoryDescription = c.String(),
            //            CategoryName = c.String(),
            //        })
            //    .PrimaryKey(t => t.BookCategoryID);
            
            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //        {
            //            ProductID = c.Int(nullable: false, identity: true),
            //            CostPrice = c.Double(nullable: false),
            //            SellingPrice = c.Double(nullable: false),
            //            DateAdded = c.DateTime(nullable: false),
            //            IsBook = c.Boolean(nullable: false),
            //            SupplierID = c.Int(nullable: false),
            //            BookID = c.Int(),
            //            ISBN = c.String(),
            //            BookTitle = c.String(),
            //            Synopsis = c.String(),
            //            PublisherID = c.Int(),
            //            BookCategoryID = c.Int(),
            //            AuthorID = c.Int(),
            //            CoverImage = c.String(),
            //            Name = c.String(),
            //            TechID = c.Int(),
            //            TechCategoryID = c.Int(),
            //            ModelName = c.String(),
            //            Specs = c.String(),
            //            ModelNumber = c.String(),
            //            ManufacturerID = c.Int(),
            //            ImageFront = c.String(),
            //            ImageTop = c.String(),
            //            ImageSide = c.String(),
            //            Discriminator = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => t.ProductID);
            
            //CreateTable(
            //    "dbo.Authors",
            //    c => new
            //        {
            //            AuthorID = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Surname = c.String(),
            //        })
            //    .PrimaryKey(t => t.AuthorID);
            
            //CreateIndex("dbo.Products", "AuthorID");
            //AddForeignKey("dbo.Products", "AuthorID", "dbo.Authors", "AuthorID", cascadeDelete: true);
        }
    }
}

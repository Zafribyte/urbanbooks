namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class msd : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.BookCategories",
            //    c => new
            //        {
            //            BookCategoryID = c.Int(nullable: false, identity: true),
            //            CategoryDescription = c.String(nullable: false),
            //            CategoryName = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.BookCategoryID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.BookCategories");
        }
    }
}

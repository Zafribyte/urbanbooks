namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pps : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Manufacturers",
            //    c => new
            //        {
            //            ManufacturerID = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.ManufacturerID);
            
            //CreateTable(
            //    "dbo.TechCategories",
            //    c => new
            //        {
            //            TechCategoryID = c.Int(nullable: false, identity: true),
            //            CategoryName = c.String(),
            //            CategoryDescription = c.String(),
            //        })
            //    .PrimaryKey(t => t.TechCategoryID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.TechCategories");
            //DropTable("dbo.Manufacturers");
        }
    }
}

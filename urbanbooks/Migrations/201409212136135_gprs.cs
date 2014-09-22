namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gprs : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Manufacturers", "Name", c => c.String(nullable: false));
            //AlterColumn("dbo.Publishers", "Name", c => c.String(nullable: false));
            //AlterColumn("dbo.TechCategories", "CategoryName", c => c.String(nullable: false));
            //AlterColumn("dbo.TechCategories", "CategoryDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.TechCategories", "CategoryDescription", c => c.String());
            //AlterColumn("dbo.TechCategories", "CategoryName", c => c.String());
            //AlterColumn("dbo.Publishers", "Name", c => c.String());
            //AlterColumn("dbo.Manufacturers", "Name", c => c.String());
        }
    }
}

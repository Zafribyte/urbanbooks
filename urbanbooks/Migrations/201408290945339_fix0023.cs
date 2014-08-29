namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix0023 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime(nullable: false));
            //AddPrimaryKey("dbo.Orders", "OrderNo");
            //DropColumn("dbo.Products", "Manufacturer");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Products", "Manufacturer", c => c.String());
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
    }
}

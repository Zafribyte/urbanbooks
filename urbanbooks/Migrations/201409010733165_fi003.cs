namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fi003 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Orders");
            //AddColumn("dbo.Orders", "InvoiceID", c => c.Int(nullable: false));
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime(nullable: false));
            //AddPrimaryKey("dbo.Orders", "OrderNo");
            //DropColumn("dbo.Orders", "DataModified");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Orders", "DataModified", c => c.DateTime(nullable: false));
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int(nullable: false, identity: true));
            //DropColumn("dbo.Orders", "InvoiceID");
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
    }
}

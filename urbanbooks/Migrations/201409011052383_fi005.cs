namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fi005 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime(nullable: false));
            //AddPrimaryKey("dbo.Orders", "OrderNo");
            //DropColumn("dbo.Orders", "OrderItemNumber");
            //DropColumn("dbo.Orders", "ProductID");
            //DropColumn("dbo.Orders", "Quantity");
            //DropColumn("dbo.Orders", "Discriminator");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Orders", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            //AddColumn("dbo.Orders", "Quantity", c => c.Int());
            //AddColumn("dbo.Orders", "ProductID", c => c.Int());
            //AddColumn("dbo.Orders", "OrderItemNumber", c => c.Int());
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int());
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
    }
}

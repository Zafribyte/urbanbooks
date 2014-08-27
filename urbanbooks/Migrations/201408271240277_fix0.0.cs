namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix00 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Orders");
            //AddColumn("dbo.Orders", "OrderItemNumber", c => c.Int());
            //AddColumn("dbo.Orders", "ProductID", c => c.Int());
            //AddColumn("dbo.Orders", "Quantity", c => c.Int());
            //AddColumn("dbo.Orders", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime());
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
        
        public override void Down()
        {
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int(nullable: false, identity: true));
            //DropColumn("dbo.Orders", "Discriminator");
            //DropColumn("dbo.Orders", "Quantity");
            //DropColumn("dbo.Orders", "ProductID");
            //DropColumn("dbo.Orders", "OrderItemNumber");
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
    }
}

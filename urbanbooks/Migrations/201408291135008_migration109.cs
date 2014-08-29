namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration109 : DbMigration
    {
        public override void Up()
        {
            //DropPrimaryKey("dbo.Orders");
            //AddColumn("dbo.Suppliers", "User_Id", c => c.String());
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime(nullable: false));
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
        
        public override void Down()
        {
            //DropPrimaryKey("dbo.Orders");
            //AlterColumn("dbo.Orders", "DateCreated", c => c.DateTime());
            //AlterColumn("dbo.Orders", "OrderNo", c => c.Int());
            //AlterColumn("dbo.Orders", "dateCreated", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.Orders", "orderNo", c => c.Int(nullable: false, identity: true));
            //DropColumn("dbo.Suppliers", "User_Id");
            //AddPrimaryKey("dbo.Orders", "OrderNo");
        }
    }
}

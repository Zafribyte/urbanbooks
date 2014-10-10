namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class context : DbMigration
    {
        public override void Up()
        {
        //    CreateTable(
        //        "dbo.Orders",
        //        c => new
        //            {
        //                OrderNo = c.Int(nullable: false, identity: true),
        //                DateCreated = c.DateTime(nullable: false),
        //                DateLastModified = c.DateTime(nullable: false),
        //                Status = c.Boolean(nullable: false),
        //                DateSent = c.DateTime(nullable: false),
        //                SupplierID = c.Int(nullable: false),
        //                EmployeeID = c.Int(nullable: false),
        //                InvoiceID = c.Int(nullable: false),
        //            })
        //        .PrimaryKey(t => t.OrderNo);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Orders");
        }
    }
}

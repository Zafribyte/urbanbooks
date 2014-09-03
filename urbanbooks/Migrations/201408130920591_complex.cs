namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complex : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Customers",
            //    c => new
            //        {
            //            CustomerID = c.Int(nullable: false, identity: true),
            //            User_Id = c.String(),
            //            FirstName = c.String(),
            //            LastName = c.String(),
            //        })
            //    .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Customers");
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supplierInterface : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.RegisterSuppliers",
            //    c => new
            //        {
            //            SupplierID = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            User_Id = c.String(),
            //            ContactPerson = c.String(nullable: false),
            //            LastName = c.String(nullable: false),
            //            Fax = c.String(nullable: false, maxLength: 10),
            //            ContactPersonNumber = c.String(nullable: false, maxLength: 10),
            //            Password = c.String(nullable: false, maxLength: 100),
            //            ConfirmPassword = c.String(),
            //            Status = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SupplierID);
            
            //AlterColumn("dbo.Suppliers", "Name", c => c.String(nullable: false));
            //AlterColumn("dbo.Suppliers", "LastName", c => c.String(nullable: false));
            //AlterColumn("dbo.Suppliers", "Fax", c => c.String(nullable: false, maxLength: 10));
            //AlterColumn("dbo.Suppliers", "ContactPerson", c => c.String(nullable: false));
            //AlterColumn("dbo.Suppliers", "ContactPersonNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Suppliers", "ContactPersonNumber", c => c.String(maxLength: 10));
            //AlterColumn("dbo.Suppliers", "ContactPerson", c => c.String());
            //AlterColumn("dbo.Suppliers", "Fax", c => c.String(maxLength: 10));
            //AlterColumn("dbo.Suppliers", "LastName", c => c.String());
            //AlterColumn("dbo.Suppliers", "Name", c => c.String());
            //DropTable("dbo.RegisterSuppliers");
        }
    }
}

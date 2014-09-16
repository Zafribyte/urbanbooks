namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supplierInterface1 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.RegisterSuppliers", "Email", c => c.String(nullable: false));
            //AddColumn("dbo.Suppliers", "Email", c => c.String(nullable: false));
            //DropColumn("dbo.RegisterSuppliers", "Status");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.RegisterSuppliers", "Status", c => c.Boolean(nullable: false));
            //DropColumn("dbo.Suppliers", "Email");
            //DropColumn("dbo.RegisterSuppliers", "Email");
        }
    }
}

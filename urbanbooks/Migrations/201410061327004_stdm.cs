namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stdm : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Suppliers", "IsBookSupplier", c => c.Boolean(nullable: false));
            //AlterColumn("dbo.RegisterSuppliers", "SupplierType", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.RegisterSuppliers", "SupplierType", c => c.String(nullable: false));
            //DropColumn("dbo.Suppliers", "IsBookSupplier");
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class st : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.RegisterSuppliers", "SupplierType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.RegisterSuppliers", "SupplierType");
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tibs : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.RegisterSuppliers", "Address", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.RegisterSuppliers", "Address");
        }
    }
}

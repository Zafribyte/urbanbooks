namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tibsx : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Suppliers", "Email");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Suppliers", "Email", c => c.String(nullable: false));
        }
    }
}

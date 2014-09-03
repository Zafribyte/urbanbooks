namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix005 : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.Wishlists", "CustomerID");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Wishlists", "CustomerID", c => c.Int(nullable: false));
        }
    }
}

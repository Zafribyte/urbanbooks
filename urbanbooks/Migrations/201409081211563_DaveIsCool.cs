namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DaveIsCool : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Book_Advanced", "RangeFrom", c => c.Double());
            //AlterColumn("dbo.Book_Advanced", "RangeTo", c => c.Double());
            //AlterColumn("dbo.Device_Advanced", "RangeFrom", c => c.Double());
            //AlterColumn("dbo.Device_Advanced", "RangeTo", c => c.Double());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Device_Advanced", "RangeTo", c => c.Double(nullable: false));
            //AlterColumn("dbo.Device_Advanced", "RangeFrom", c => c.Double(nullable: false));
            //AlterColumn("dbo.Book_Advanced", "RangeTo", c => c.Double(nullable: false));
            //AlterColumn("dbo.Book_Advanced", "RangeFrom", c => c.Double(nullable: false));
        }
    }
}

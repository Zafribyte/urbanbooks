namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gto : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.RangeValidates", "From", c => c.DateTime(nullable: false));
            //AlterColumn("dbo.RangeValidates", "To", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.RangeValidates", "To", c => c.String());
            //AlterColumn("dbo.RangeValidates", "From", c => c.String(nullable: false));
        }
    }
}

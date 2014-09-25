namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gfr : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.RangeValidates",
            //    c => new
            //        {
            //            TheKey = c.Int(nullable: false, identity: true),
            //            From = c.DateTime(nullable: false),
            //            To = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.TheKey);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.RangeValidates");
        }
    }
}

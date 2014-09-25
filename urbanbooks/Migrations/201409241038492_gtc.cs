namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gtc : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
                //"dbo.RangeValidates",
                //c => new
                //    {
                //        TheKey = c.Int(nullable: false, identity: true),
                //        From = c.String(nullable: false),
                //        To = c.String(),
                //    })
                //.PrimaryKey(t => t.TheKey);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.RangeValidates");
        }
    }
}

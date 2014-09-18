namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bs : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Manufacturers",
            //    c => new
            //        {
            //            ManufacturerID = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //        })
            //    .PrimaryKey(t => t.ManufacturerID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Manufacturers");
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mte : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Authors",
            //    c => new
            //        {
            //            AuthorID = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Surname = c.String(),
            //        })
            //    .PrimaryKey(t => t.AuthorID);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Authors");
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lkj : DbMigration
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
            
            //CreateIndex("dbo.Products", "AuthorID");
            //AddForeignKey("dbo.Products", "AuthorID", "dbo.Authors", "AuthorID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Products", "AuthorID", "dbo.Authors");
            //DropIndex("dbo.Products", new[] { "AuthorID" });
            //DropTable("dbo.Authors");
        }
    }
}

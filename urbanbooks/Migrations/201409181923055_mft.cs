namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mft : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Companies",
            //    c => new
            //        {
            //            CompanyRegistration = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            TaxRefferenceNumber = c.String(nullable: false),
            //            VATRegistrationNumber = c.String(nullable: false),
            //            VATPercentage = c.Double(nullable: false),
            //            BookMarkUp = c.Double(nullable: false),
            //            TechMarkUp = c.Double(nullable: false),
            //            Address = c.String(nullable: false),
            //            Telephone = c.String(nullable: false),
            //            Fax = c.String(nullable: false),
            //            Email = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.CompanyRegistration);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.Companies");
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class error : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.FullRegisterViewModels", "FirstName", c => c.String(maxLength: 150));
            //AlterColumn("dbo.FullRegisterViewModels", "LastName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.FullRegisterViewModels", "LastName", c => c.String(nullable: false, maxLength: 150));
            //AlterColumn("dbo.FullRegisterViewModels", "FirstName", c => c.String(nullable: false, maxLength: 150));
        }
    }
}

namespace urbanbooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erro : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.FullRegisterViewModels", "Address", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.FullRegisterViewModels", "Address", c => c.String(nullable: false, maxLength: 150));
        }
    }
}

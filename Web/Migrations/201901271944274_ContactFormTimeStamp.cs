namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactFormTimeStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactDataModels", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactDataModels", "DateTime");
        }
    }
}

namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDataModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Category = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactDataModels");
        }
    }
}

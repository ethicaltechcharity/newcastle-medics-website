namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegistrationDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberRegistrationDataModels", "RegistrationType", c => c.String());
            AddColumn("dbo.MemberRegistrationDataModels", "ApparentSquad", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberRegistrationDataModels", "ApparentSquad");
            DropColumn("dbo.MemberRegistrationDataModels", "RegistrationType");
        }
    }
}

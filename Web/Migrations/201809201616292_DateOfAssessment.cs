namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateOfAssessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrialAssessmentDataModels", "DateOfAssessment", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrialAssessmentDataModels", "DateOfAssessment");
        }
    }
}

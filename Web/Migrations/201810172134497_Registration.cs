namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Registration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberLegalDataModels",
                c => new
                    {
                        MemberLegalId = c.Int(nullable: false, identity: true),
                        DataConsent = c.Boolean(nullable: false),
                        AgreesCodeOfConduct = c.Boolean(nullable: false),
                        DateOfConsent = c.DateTime(nullable: false),
                        IdentityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberLegalId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.PlayingHistoryDataModels",
                c => new
                    {
                        PlayingHistoryId = c.Int(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        NewPlayer = c.Boolean(nullable: false),
                        Team = c.String(maxLength: 4000),
                        Club = c.String(maxLength: 4000),
                        TimeFrame = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.PlayingHistoryId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.PlayingShirtDataModels",
                c => new
                    {
                        PlayingShirtId = c.Int(nullable: false, identity: true),
                        PlayingShirtNumber = c.Int(nullable: false),
                        IdentityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayingShirtId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.MemberRegistrationDataModels",
                c => new
                    {
                        MemberRegistrationId = c.Int(nullable: false, identity: true),
                        Season = c.String(maxLength: 4000),
                        DateOfRegistration = c.DateTime(nullable: false),
                        PaidSubscription = c.Boolean(nullable: false),
                        IdentityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberRegistrationId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.UmpireDataModels",
                c => new
                    {
                        UmpireId = c.Int(nullable: false, identity: true),
                        UmpiringLevel = c.String(maxLength: 4000),
                        UmpiringNumber = c.String(maxLength: 4000),
                        IdentityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UmpireId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UmpireDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.MemberRegistrationDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.PlayingShirtDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.PlayingHistoryDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.MemberLegalDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropIndex("dbo.UmpireDataModels", new[] { "IdentityId" });
            DropIndex("dbo.MemberRegistrationDataModels", new[] { "IdentityId" });
            DropIndex("dbo.PlayingShirtDataModels", new[] { "IdentityId" });
            DropIndex("dbo.PlayingHistoryDataModels", new[] { "IdentityId" });
            DropIndex("dbo.MemberLegalDataModels", new[] { "IdentityId" });
            DropTable("dbo.UmpireDataModels");
            DropTable("dbo.MemberRegistrationDataModels");
            DropTable("dbo.PlayingShirtDataModels");
            DropTable("dbo.PlayingHistoryDataModels");
            DropTable("dbo.MemberLegalDataModels");
        }
    }
}

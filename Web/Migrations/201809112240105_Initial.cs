namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpressInterestDataModels",
                c => new
                    {
                        ExpressInterestId = c.Int(nullable: false, identity: true),
                        PlayedBefore = c.Boolean(nullable: false),
                        Position = c.String(),
                        IdentityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpressInterestId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.MemberIdentityDataModels",
                c => new
                    {
                        MemberIdentityId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.MemberIdentityId);
            
            CreateTable(
                "dbo.MemberRoleDataModels",
                c => new
                    {
                        MemberRoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MemberRoleId);
            
            CreateTable(
                "dbo.NumberAssignmentDataModels",
                c => new
                    {
                        NumberAssignmentId = c.Int(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        AssignedNumber = c.Int(nullable: false),
                        DateOfAssignment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NumberAssignmentId)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TrialAssessmentDataModels",
                c => new
                    {
                        TrialAssessmentId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        AssignedNumber = c.Int(nullable: false),
                        Drill = c.String(),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrialAssessmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MemberRoleDataModelMemberIdentityDataModels",
                c => new
                    {
                        MemberRoleDataModel_MemberRoleId = c.Int(nullable: false),
                        MemberIdentityDataModel_MemberIdentityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberRoleDataModel_MemberRoleId, t.MemberIdentityDataModel_MemberIdentityId })
                .ForeignKey("dbo.MemberRoleDataModels", t => t.MemberRoleDataModel_MemberRoleId, cascadeDelete: true)
                .ForeignKey("dbo.MemberIdentityDataModels", t => t.MemberIdentityDataModel_MemberIdentityId, cascadeDelete: true)
                .Index(t => t.MemberRoleDataModel_MemberRoleId)
                .Index(t => t.MemberIdentityDataModel_MemberIdentityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrialAssessmentDataModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.NumberAssignmentDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.ExpressInterestDataModels", "IdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.MemberRoleDataModelMemberIdentityDataModels", "MemberIdentityDataModel_MemberIdentityId", "dbo.MemberIdentityDataModels");
            DropForeignKey("dbo.MemberRoleDataModelMemberIdentityDataModels", "MemberRoleDataModel_MemberRoleId", "dbo.MemberRoleDataModels");
            DropIndex("dbo.MemberRoleDataModelMemberIdentityDataModels", new[] { "MemberIdentityDataModel_MemberIdentityId" });
            DropIndex("dbo.MemberRoleDataModelMemberIdentityDataModels", new[] { "MemberRoleDataModel_MemberRoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TrialAssessmentDataModels", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NumberAssignmentDataModels", new[] { "IdentityId" });
            DropIndex("dbo.ExpressInterestDataModels", new[] { "IdentityId" });
            DropTable("dbo.MemberRoleDataModelMemberIdentityDataModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TrialAssessmentDataModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NumberAssignmentDataModels");
            DropTable("dbo.MemberRoleDataModels");
            DropTable("dbo.MemberIdentityDataModels");
            DropTable("dbo.ExpressInterestDataModels");
        }
    }
}

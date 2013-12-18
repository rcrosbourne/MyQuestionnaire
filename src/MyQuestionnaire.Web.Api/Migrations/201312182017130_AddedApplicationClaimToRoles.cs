namespace MyQuestionnaire.Web.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationClaimToRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRoleApplicationClaims",
                c => new
                    {
                        ApplicationRole_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationClaim_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationRole_Id, t.ApplicationClaim_Id })
                .ForeignKey("dbo.AspNetRoles", t => t.ApplicationRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationClaims", t => t.ApplicationClaim_Id, cascadeDelete: true)
                .Index(t => t.ApplicationRole_Id)
                .Index(t => t.ApplicationClaim_Id);
            
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationRoleApplicationClaims", "ApplicationClaim_Id", "dbo.ApplicationClaims");
            DropForeignKey("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationClaim_Id" });
            DropIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_Id" });
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropTable("dbo.ApplicationRoleApplicationClaims");
        }
    }
}

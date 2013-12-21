namespace MyQuestionnaire.Web.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertRoleToTakeIdenityRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" }, "dbo.AspNetUserRoles");
            DropIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" });
            RenameColumn(table: "dbo.ApplicationRoleApplicationClaims", name: "ApplicationRole_UserId", newName: "ApplicationRole_Id");
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String());
            DropPrimaryKey("dbo.ApplicationRoleApplicationClaims");
            AddPrimaryKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_Id", "ApplicationClaim_Id" });
            CreateIndex("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id");
            AddForeignKey("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUserRoles", "Discriminator");
            DropColumn("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_RoleId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_Id" });
            DropPrimaryKey("dbo.ApplicationRoleApplicationClaims");
            AddPrimaryKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId", "ApplicationClaim_Id" });
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.AspNetRoles", "Discriminator");
            RenameColumn(table: "dbo.ApplicationRoleApplicationClaims", name: "ApplicationRole_Id", newName: "ApplicationRole_UserId");
            CreateIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" });
            AddForeignKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" }, "dbo.AspNetUserRoles", new[] { "UserId", "RoleId" }, cascadeDelete: true);
        }
    }
}

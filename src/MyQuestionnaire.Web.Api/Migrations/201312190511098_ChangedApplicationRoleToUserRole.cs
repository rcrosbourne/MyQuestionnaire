namespace MyQuestionnaire.Web.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedApplicationRoleToUserRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id", "dbo.AspNetRoles");
            DropIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_Id" });
            RenameColumn(table: "dbo.ApplicationRoleApplicationClaims", name: "ApplicationRole_Id", newName: "ApplicationRole_UserId");
            AddColumn("dbo.AspNetUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false));
            DropPrimaryKey("dbo.ApplicationRoleApplicationClaims");
            AddPrimaryKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId", "ApplicationClaim_Id" });
            CreateIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" });
            AddForeignKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" }, "dbo.AspNetUserRoles", new[] { "UserId", "RoleId" }, cascadeDelete: true);
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" }, "dbo.AspNetUserRoles");
            DropIndex("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_UserId", "ApplicationRole_RoleId" });
            DropPrimaryKey("dbo.ApplicationRoleApplicationClaims");
            AddPrimaryKey("dbo.ApplicationRoleApplicationClaims", new[] { "ApplicationRole_Id", "ApplicationClaim_Id" });
            AlterColumn("dbo.AspNetUsers", "UserName", c => c.String());
            DropColumn("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_RoleId");
            DropColumn("dbo.AspNetUserRoles", "Discriminator");
            RenameColumn(table: "dbo.ApplicationRoleApplicationClaims", name: "ApplicationRole_UserId", newName: "ApplicationRole_Id");
            CreateIndex("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id");
            AddForeignKey("dbo.ApplicationRoleApplicationClaims", "ApplicationRole_Id", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}

namespace MyQuestionnaire.Web.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAttributeOnApplicationClaim : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationClaims", "ClaimType", c => c.String(nullable: false));
            AlterColumn("dbo.ApplicationClaims", "ClaimValue", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationClaims", "ClaimValue", c => c.String());
            AlterColumn("dbo.ApplicationClaims", "ClaimType", c => c.String());
        }
    }
}

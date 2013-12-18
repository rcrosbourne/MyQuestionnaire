namespace MyQuestionnaire.Web.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedApplicationClaim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(nullable: false),
                        ClaimValue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationClaims");
        }
    }
}

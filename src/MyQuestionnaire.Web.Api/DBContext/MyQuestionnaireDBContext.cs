using System.Data.Entity;
using log4net;
using Microsoft.AspNet.Identity.EntityFramework;
using MyQuestionnaire.Web.Api.Models;
using MyQuestionnaire.Web.Common;

namespace MyQuestionnaire.Web.Api.DBContext
{
    public class MyQuestionnaireDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        public MyQuestionnaireDbContext(ILog logger) : base("name=MyQuestionnaireDBContext")
        {
            Database.Log = logger.Debug;
        }

        public MyQuestionnaireDbContext()
            : base("name=MyQuestionnaireDBContext")
        {
            
        }
        //public MyQuestionnaireDbContext() : this(WebContainerManager.Get<ILog>())
        //{
        //    //var dbLog = WebContainerManager.Get<ILog>();
        //    //Database.Log = dbLog.Debug; //Log database queries
        //}
        
        public DbSet<OpenEndedQuestion> OpenEndedQuestions { get; set; }
        public DbSet<ApplicationClaim> ApplicationClaims { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApiClient> ApiClients { get; set; }
       // public DbSet<RoleClaim> RoleClaims { get; set; }
    }
}

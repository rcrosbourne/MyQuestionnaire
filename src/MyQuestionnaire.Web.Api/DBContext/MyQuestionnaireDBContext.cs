using System.Data.Entity;
using log4net;
using MyQuestionnaire.Web.Api.Models;
using MyQuestionnaire.Web.Common;

namespace MyQuestionnaire.Web.Api.DBContext
{
    public class MyQuestionnaireDbContext : DbContext, IDbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //private ILog _dbLog;
    
        public MyQuestionnaireDbContext() : base("name=MyQuestionnaireDBContext")
        {
            var _dbLog = WebContainerManager.Get<ILog>();
            Database.Log = _dbLog.Debug; //Log database queries
        }
        
        public DbSet<OpenEndedQuestion> OpenEndedQuestions { get; set; }
    
    }
}

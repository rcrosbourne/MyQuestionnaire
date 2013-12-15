using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyQuestionnaire.Web.Api.Models;

namespace MyQuestionnaire.Web.Api.DBContext
{
    public interface IDbContext : IDisposable
    {
        DbSet<OpenEndedQuestion> OpenEndedQuestions { get; set; }
        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}

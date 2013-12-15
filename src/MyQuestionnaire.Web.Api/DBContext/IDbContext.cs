using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

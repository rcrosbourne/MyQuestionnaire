using MyQuestionnaire.Web.Api.Models;

namespace MyQuestionnaire.Web.Api.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DBContext.MyQuestionnaireDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DBContext.MyQuestionnaireDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.OpenEndedQuestions.AddOrUpdate
                (
                    q => q.Description,
                    new OpenEndedQuestion {Description = "This is my first seeded question", Text = "Ask me anything", Answers = "How are u alive|Why are you here"},
                    new OpenEndedQuestion {Description = "This is my second seeded question", Text = "Ask me another question", Answers = "I like cookies"},
                    new OpenEndedQuestion {Description = "This is my third seeded question", Text = "Ask me a third", Answers = "This is actually better"},
                    new OpenEndedQuestion {Description = "This is my fourth seeded question", Text = "what is the final Question", Answers = "Do you believe"}
                );
        }
    }
}

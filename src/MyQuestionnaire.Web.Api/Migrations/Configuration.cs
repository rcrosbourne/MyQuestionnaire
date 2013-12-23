using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyQuestionnaire.Web.Api.Models;
using WebGrease.Css.Extensions;

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
            //context.OpenEndedQuestions.AddOrUpdate
            //    (
            //        q => q.Description,
            //        new OpenEndedQuestion {Description = "This is my first seeded question", Text = "Ask me anything", Answers = "How are u alive|Why are you here"},
            //        new OpenEndedQuestion {Description = "This is my second seeded question", Text = "Ask me another question", Answers = "I like cookies"},
            //        new OpenEndedQuestion {Description = "This is my third seeded question", Text = "Ask me a third", Answers = "This is actually better"},
            //        new OpenEndedQuestion {Description = "This is my fourth seeded question", Text = "what is the final Question", Answers = "Do you believe"}
            //    );
            try
            {
                var openEndedQuestions = new List<OpenEndedQuestion>
                {
                    new OpenEndedQuestion
                    {
                        Description = "This is my first seeded question",
                        Text = "Ask me anything",
                        Answers = "How are u alive|Why are you here"
                    },
                    new OpenEndedQuestion
                    {
                        Description = "This is my second seeded question",
                        Text = "Ask me another question",
                        Answers = "I like cookies"
                    },
                    new OpenEndedQuestion
                    {
                        Description = "This is my third seeded question",
                        Text = "Ask me a third",
                        Answers = "This is actually better"
                    },
                    new OpenEndedQuestion
                    {
                        Description = "This is my fourth seeded question",
                        Text = "what is the final Question",
                        Answers = "Do you believe"
                    }

                };
                openEndedQuestions.ForEach(o => context.OpenEndedQuestions.AddOrUpdate(q => new {q.Text, q.Description,q.Answers}, o));
                context.SaveChanges();

                var claims = new List<ApplicationClaim>
                {
                    SetupApplicationClaims.GetAllOpenEndedQuestion(),
                    SetupApplicationClaims.GetOneOpenEndedQuestion(),
                    SetupApplicationClaims.PutOpenEndedQuestion(),
                    SetupApplicationClaims.PostOpenEndedQuestion(),
                    SetupApplicationClaims.DeleteOpenEndedQuestion()
                };
                claims.ForEach(c => context.ApplicationClaims.AddOrUpdate(ac => new { ac.ClaimType, ac.ClaimValue }, c));
                context.SaveChanges();
                //Add Roles
                var adminRole = new ApplicationRole
                {
                    Name = "Admin"
                };
                var defaultRole = new ApplicationRole
                {
                    Name = "Default"
                };


                context.ApplicationClaims.ForEach(c => adminRole.ApplicationClaims.Add(c));
                
                context.ApplicationClaims.ForEach(c =>
                {
                    if (c.ClaimType.StartsWith("Get"))
                    {
                        defaultRole.ApplicationClaims.Add(c); //Read only access to default role
                    }
                    
                });
                context.ApplicationRoles.AddOrUpdate(r => r.Name, adminRole);
                context.ApplicationRoles.AddOrUpdate(r => r.Name, defaultRole);
                context.SaveChanges();


                //Create bindings between Roles and Claims
               // Give read access to default...give all to admin
                var newUser = new ApplicationUser
                {
                    UserName = "rcrosbourne"
                    
                };
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

                if (userManager.FindByName(newUser.UserName) == null)
                {
                    userManager.Create(newUser, "password123");
                    userManager.AddToRole(newUser.Id, adminRole.Name);
                    userManager.AddToRole(newUser.Id, defaultRole.Name);
                }
                context.ApiClients.AddOrUpdate( client => client.Name, 
                    new ApiClient
                    {
                        Name = "WebClient",
                        IsBlacklisted = false
                    });
                context.SaveChanges();
            
                
            }
            catch (DbEntityValidationException dbValEx)
            {
                var outputLines = new StringBuilder();
                foreach (var eve in dbValEx.EntityValidationErrors)
                {
                    outputLines.AppendFormat("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:"
                      , DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.AppendFormat("- Property: \"{0}\", Error: \"{1}\""
                         , ve.PropertyName, ve.ErrorMessage);
                    }
                }

                throw new DbEntityValidationException(string.Format("Validation errors\r\n{0}"
                 , outputLines), dbValEx);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
                openEndedQuestions.ForEach(o => context.OpenEndedQuestions.Add(o));
                context.SaveChanges();

                var claims = new List<ApplicationClaim>
                {
                    SetupApplicationClaims.AllOnAll(),
                    SetupApplicationClaims.AllOnOpenEndedQuestion(),
                    SetupApplicationClaims.CreateOpenEndedQuestion(),
                    SetupApplicationClaims.ReadAllOpenEndedQuestion(),
                    SetupApplicationClaims.ReadByIdOpenEndedQuestion(),
                    SetupApplicationClaims.UpdateAllOpenEndedQuestion(),
                    SetupApplicationClaims.UpdateAllOpenEndedQuestion(),
                    SetupApplicationClaims.UpdateByIdOpenEndedQuestion()
                };
                claims.ForEach(c => context.ApplicationClaims.Add(c));
                context.SaveChanges();
                //Add Roles
                var adminRole = new ApplicationRole()
                {
                    Name = "Admin"
                };
                var defaultRole = new ApplicationRole()
                {
                    Name = "Default"
                };

                adminRole.ApplicationClaims.Add(context.ApplicationClaims.SingleOrDefault(c => c.Id == 1));
                context.ApplicationRoles.Add(adminRole);
                context.ApplicationRoles.Add(defaultRole);
                context.SaveChanges();


                //Create bindings between Roles and Claims
               // Give read access to default...give all to admin
                //var adminRole = context.ApplicationRoles.SingleOrDefault(ar => ar.Name == "Admin");
                //Add All claim to Admin
                //GetAll claim 
               
                context.SaveChanges();
                var newUser = new ApplicationUser()
                {
                    UserName = "rcrosbourne"
                };
                //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
                //await userManager.CreateAsync(newUser, "password123");
            
                newUser.Roles.Add(new IdentityUserRole()
                {
                    Role = adminRole
                });
                context.Users.Add(newUser);
                
                context.ApiClients.Add(new ApiClient()
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

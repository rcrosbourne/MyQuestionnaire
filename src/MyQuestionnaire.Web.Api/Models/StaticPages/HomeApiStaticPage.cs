using System.Collections.Generic;
using MyQuestionnaire.Web.Api.ViewModels;

namespace MyQuestionnaire.Web.Api.Models.StaticPages
{
    public class HomeApiStaticPage 
    {
        public List<Link> ClientAdministraionLinks { get; set; }
        public List<Link> UserAccountAdministrationLinks { get; set; }
        public List<Link> QuestionnaireAdministraionLinks { get; set; }
        public HomeApiStaticPage()
        {
            ClientAdministraionLinks = new List<Link>
            {
                new Link
                {
                    Title = "Client Administration",
                    Href = "api/clients"
                }
            };
            UserAccountAdministrationLinks = new List<Link>
            {
                new Link
                {
                    Title = "User Account Administration",
                    Href = "api/applicationusers"
                }
            };
            QuestionnaireAdministraionLinks = new List<Link>
            {
                new Link
                {
                    Title = "Questionnaire Administration",
                    Href = "api/questionnaires"
                }
            };
        }
    }
}
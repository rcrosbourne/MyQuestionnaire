using System.Collections.Generic;

namespace MyQuestionnaire.Web.Api.ViewModels
{
    public class OpenEndedQuestionViewModel
    {
 
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public List<string> Answers { get; set; }
        public List<Link> Links { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyQuestionnaire.Web.Api.ViewModels
{
    public class OpenEndedQuestionViewModel : ViewModelBase
    {
 
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public string Description { get; set; }
        private List<string> _answers = new List<string>();
        public List<string> Answers
        {
            get { return _answers; } 
            set { _answers = value; }
        }
        [ReadOnly(true)]
        private List<Link> _links = new List<Link>();
        public List<Link> Links
        {
            get { return _links; }
            set { _links = value; }
        }
    }
}
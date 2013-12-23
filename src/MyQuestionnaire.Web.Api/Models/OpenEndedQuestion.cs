using System.ComponentModel.DataAnnotations;

namespace MyQuestionnaire.Web.Api.Models
{
    public class OpenEndedQuestion : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public string Description { get; set; }
        public string Answers { get; set; }
    }
}
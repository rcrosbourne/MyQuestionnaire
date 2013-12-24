using System.Collections.Generic;
using System.Linq;
using MyQuestionnaire.Web.Api.Models;
using MyQuestionnaire.Web.Api.ViewModels;

namespace MyQuestionnaire.Web.Api.TypeMappers
{
    public class OpenEndedQuestionMap : IOpenEndedQuestionMap
    {
        public OpenEndedQuestionViewModel CreateViewModel(OpenEndedQuestion model)
        {
            return new OpenEndedQuestionViewModel()
            {
                Id = model.Id,
                Description = model.Description,
                Text = model.Text,
                Timestamp = model.Timestamp,
                Answers = model.Answers.Split('|').ToList(),
                Links = new List<Link>
                            {
                                new Link()
                                {
                                    Title = "self",
                                    Href = "/api/openendedquestion/" + model.Id
                                               
                                },
                                new Link()
                                {
                                    Title = "All Open Ended Questions",
                                    Href = "/api/openendedquestion"
                                }

                            }
            };
        }

        public OpenEndedQuestion CreateModel(OpenEndedQuestionViewModel viewModel)
        {
            return new OpenEndedQuestion()
            {
                Id = viewModel.Id,
                Description = viewModel.Description,
                Text =  viewModel.Text,
                Answers = string.Join("|", viewModel.Answers),
                Timestamp = viewModel.Timestamp
                
            };
        }
    }
}
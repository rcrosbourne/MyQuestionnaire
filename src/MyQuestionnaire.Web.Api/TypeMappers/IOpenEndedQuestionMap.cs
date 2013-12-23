using MyQuestionnaire.Web.Api.Models;
using MyQuestionnaire.Web.Api.ViewModels;

namespace MyQuestionnaire.Web.Api.TypeMappers
{
    public interface IOpenEndedQuestionMap
    {
        OpenEndedQuestionViewModel CreateViewModel(OpenEndedQuestion model);
        OpenEndedQuestion CreateModel(OpenEndedQuestionViewModel viewModel);
    }
}
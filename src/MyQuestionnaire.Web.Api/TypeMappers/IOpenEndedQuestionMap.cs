using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Web.Http.Filters;

namespace MyQuestionnaire.Web.Common
{
    public interface  IActionExceptionHandler
    {
        void HandleException(HttpActionExecutedContext filterContext);
    }
}

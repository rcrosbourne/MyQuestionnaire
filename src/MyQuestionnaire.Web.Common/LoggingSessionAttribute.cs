using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity;

namespace MyQuestionnaire.Web.Common
{
    public class LoggingSessionAttribute : ActionFilterAttribute
    {
        private readonly IActionLogHelper _actionLogHelper;
        private readonly IActionExceptionHandler _actionExceptionHandler;
        

        public LoggingSessionAttribute()
            : this(WebContainerManager.Get<IActionLogHelper>(),
            WebContainerManager.Get<IActionExceptionHandler>())
        {
        }
        public LoggingSessionAttribute(
            IActionLogHelper actionLogHelper, 
            IActionExceptionHandler actionExceptionHandler)
        {
            _actionLogHelper = actionLogHelper;
            _actionExceptionHandler = actionExceptionHandler;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _actionLogHelper.LogEntry(actionContext.ActionDescriptor);
        }
        
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _actionExceptionHandler.HandleException(actionExecutedContext);
            _actionLogHelper.LogExit(actionExecutedContext.ActionContext.ActionDescriptor);
        }
    }
}

using System;

namespace MyQuestionnaire.Web.Common
{
    public interface IExceptionMessageFormatter
    {
        string GetEntireExceptionStack(Exception ex);
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MissionReportingTool.Exceptions
{
    public class BaseHttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BaseHttpResponseException httpResponseException)
            {
                context.Result = new JsonResult(new {
                    error = httpResponseException.Message,
                    exceptionType = httpResponseException.GetType().Name
                })
                {
                    StatusCode = (int) httpResponseException.StatusCode
                };

                context.ExceptionHandled = true;
            }
        }
    }
}

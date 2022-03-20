using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Filters
{
    public class ApiExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            if (context.Exception !=null && context.Exception.Message != null)
            {
                context.Result = new ObjectResult(new ApiResponse<ActionError> { Data = null, Message = context.Exception.Message, Success = false, Status=400 });
                context.ExceptionHandled = true;

            }
        }
    }

    public class ActionError
    {

    }
}

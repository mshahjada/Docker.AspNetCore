using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CloudApp.Filters
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Exit: " + MethodBase.GetCurrentMethod().Name, context.HttpContext.Request.Path);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Enter: " + MethodBase.GetCurrentMethod().Name, context.HttpContext.Request.Path);
        }
    }
}

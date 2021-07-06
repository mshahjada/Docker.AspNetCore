using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Filters
{
    public class GlobalActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            Console.WriteLine("Exit: " + MethodBase.GetCurrentMethod().Name, actionExecutedContext.HttpContext.Request.Path);

            var data = GetJsonDataFromRequestBodyAsync(actionExecutedContext.HttpContext.Request);
        }

        public void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            Console.WriteLine("Enter: " + MethodBase.GetCurrentMethod().Name, actionExecutingContext.HttpContext.Request.Path);
        }


        private string GetJsonDataFromRequestBodyAsync(HttpRequest request)
        {

            string reqBody = default;

            string method = request.Method.ToLower();

            if(method.Equals("put") || method.Equals("post"))
            {
                //stream.BaseStream.Seek(0, SeekOrigin.Begin);
                request.Body.Seek(0, SeekOrigin.Begin);
                using (var stream = new StreamReader(request.Body, encoding: Encoding.UTF8))
                {
                    reqBody = stream.ReadToEnd();
                }
            }
            return reqBody;
        }


    }
}

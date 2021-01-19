using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudApp.Filters
{
    public class AddHeaderAttribute: ResultFilterAttribute
    {
        private readonly string _key;
        private readonly string _value;

        public AddHeaderAttribute(string key, string value) { _key = key; _value = value; }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_key, _value);
            base.OnResultExecuting(context);
        }

    }
}

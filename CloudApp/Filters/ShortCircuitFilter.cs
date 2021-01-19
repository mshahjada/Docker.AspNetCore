﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudApp.Filters
{
    public class ShortCircuitFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
           
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {

            context.Result = new ContentResult()
            {
                Content = "It's Break"
            };
        }
    }
}

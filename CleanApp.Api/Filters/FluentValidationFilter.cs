using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CleanApp.Api.Filters
{
    public class FluentValidationFilter: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x=>x.Errors)
                .Select(x=>x.ErrorMessage).ToList();
                    

                var resulModel=ServiceResult.Fail(errors);
                context.Result = new BadRequestObjectResult(resulModel);
                return;
            }


            await next();
        }
    }
   
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;

namespace Vezeeta.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute

    {



        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {

                context.Result = new BadRequestObjectResult(new ErrorResponse {statusCode= 400 ,  Errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });
            }
        }
    }
}

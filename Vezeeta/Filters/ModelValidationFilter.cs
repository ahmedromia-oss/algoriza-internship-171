using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;
using Core.Models;

namespace Vezeeta.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute

    {



        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {

                context.Result = new BadRequestObjectResult(new ErrorResponse {statusCode= Convert.ToInt32(Enums.StatusCode.BadRequest) ,  Errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList() });
            }
        }
    }
}

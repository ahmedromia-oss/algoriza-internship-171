using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Repository;

namespace Vezeeta.Filters
{

    public class UnitOfWorkFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork unitOfWork;


        
        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            

        }

        public override async void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;
            if (context.Exception == null && context.ModelState.IsValid)
            {
                await this.unitOfWork.commitTransaction();
            }
            else
            {
                await this.unitOfWork.RollBack();
            }
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase))
                return;
            await this.unitOfWork.startTransaction();
        }
    }
    
}

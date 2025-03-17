using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the model state is valid
            if (!context.ModelState.IsValid)
            {
                // If the model state is invalid, return a 400 Bad Request with error details
                context.Result = new BadRequestObjectResult(new
                {
                    Message = "The request data is invalid.",
                    Errors = context.ModelState
                        .Where(e => e.Value !=null&& e.Value.Errors.Any())
                        .ToDictionary(
                            e => e.Key,
                            e => e.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                        )
                });
                base.OnActionExecuting(context);
            }
        }
    }
}

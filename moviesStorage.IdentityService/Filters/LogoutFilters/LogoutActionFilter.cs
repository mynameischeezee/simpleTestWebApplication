using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace moviesStorage.IdentityService.Filters.LogoutFilters;

public class LogoutActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        Log.Information($"Attempting to logout user.");
        base.OnActionExecuting(actionContext);
    }

    public override void OnActionExecuted(ActionExecutedContext actionContext)
    {
        if (actionContext.Exception != null)
        {
            Log.Error("User logout failed.");
            base.OnActionExecuted(actionContext);
            return;
        }
        
        Log.Information($"User logout in successfully");
        base.OnActionExecuted(actionContext);
    }
}
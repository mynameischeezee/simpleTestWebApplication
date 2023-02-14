using Microsoft.AspNetCore.Mvc.Filters;
using moviesStorage.IdentityService.Models;
using Serilog;

namespace moviesStorage.IdentityService.Filters.LoginFilters;

public class LoginActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var user = actionContext.ActionArguments.FirstOrDefault(u => u.Value.GetType() == typeof(UserLoginDTO))
            .Value as UserLoginDTO; 
        Log.Information($"Attempting to login user: {user.Username}");
        base.OnActionExecuting(actionContext);
    }

    public override void OnActionExecuted(ActionExecutedContext actionContext)
    {
        if (actionContext.Exception != null)
        {
            Log.Error("User login failed.");
            base.OnActionExecuted(actionContext);
            return;
        }
        
        Log.Information($"User logged in successfully");
        base.OnActionExecuted(actionContext);
    }
}
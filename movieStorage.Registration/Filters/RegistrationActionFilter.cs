using Microsoft.AspNetCore.Mvc.Filters;
using movieStorage.Registration.Models;
using Serilog;

namespace movieStorage.Registration.Filters;

public class RegistrationActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var userDTO = actionContext.ActionArguments.FirstOrDefault(u => u.Value.GetType() == typeof(UserDTO))
            .Value as UserDTO; 
        Log.Information($"Attempting to register user: {userDTO.Username} with email address: {userDTO.Email}");
        base.OnActionExecuting(actionContext);
    }

    public override void OnActionExecuted(ActionExecutedContext actionContext)
    {
        if (actionContext.Exception != null)
        {
            Log.Error("User registration failed.");
            base.OnActionExecuted(actionContext);
            return;
        }
        
        Log.Information($"User with email address registered successfully");
        base.OnActionExecuted(actionContext);
    }
    
}
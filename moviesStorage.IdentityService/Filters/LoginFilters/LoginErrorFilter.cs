using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using moviesStorage.IdentityService.Exceptions;
using moviesStorage.IdentityService.Exceptions.LoginExceptions;
using Serilog;

namespace moviesStorage.IdentityService.Filters.LoginFilters;

public class LoginErrorFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case WrongCredentialsException wrongCredentialsException:
            {
                Log.Error($"Failed to login user. Inputted values are not valid: {wrongCredentialsException.Message}");
                context.Result = new JsonResult(wrongCredentialsException.Message)
                {
                    StatusCode = 400
                };
                break;
            }
            case ModelInvalidException modelInvalidException:
                Log.Error($"Failed to login user. Model is invalid: {modelInvalidException.Message}");
                context.Result = context.Result = new JsonResult(modelInvalidException.Message)
                {
                    StatusCode = 400
                };
                break;
            case UserNotExistException userNotExistException:
                Log.Error($"Failed to login user. {userNotExistException.Message}");
                context.Result = context.Result = new JsonResult(userNotExistException.Message)
                {
                    StatusCode = 400
                };
                break;
            default:
                Log.Error($"Failed to login user. Exception is unhandled: {context.Exception.Message}");
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                break;
        }
        base.OnException(context);
    }
}
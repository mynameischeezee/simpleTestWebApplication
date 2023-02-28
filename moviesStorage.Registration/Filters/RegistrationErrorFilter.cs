using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using moviesStorage.Registration.Exceptions;
using Serilog;

namespace moviesStorage.Registration.Filters;

public class RegistrationErrorFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case RegistrationFailedException registrationFailedException:
            {
                var errors = registrationFailedException.Errors;
                Log.Error($"Failed to register user. Inputted values are not valid: {registrationFailedException.Errors}");
                context.Result = new JsonResult(errors)
                {
                    StatusCode = 400
                };
                break;
            }
            case ModelInvalidException modelInvalidException:
                Log.Error($"Failed to register user. Model is invalid: {modelInvalidException.Message}");
                context.Result = context.Result = new JsonResult(modelInvalidException.Message)
                {
                    StatusCode = 400
                };
                break;
            case SqlException sqlException:
                Log.Error($"Failed to register user. Can't produce SQL query: {sqlException.Errors}");
                context.Result = new JsonResult(sqlException.Errors)
                {
                    StatusCode = 400
                };
                break;
            default:
                Log.Error($"Failed to register user. Exception is unhandled: {context.Exception.Message}");
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                break;
        }
        base.OnException(context);
    }
}
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using movieStorage.Registration.Exceptions;
using Serilog;

namespace movieStorage.Registration.Filters;

public class RegistrationErrorFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext exceptionContext)
    {
        if (exceptionContext.Exception is RegistrationFailedException registrationFailedException)
        {
            var errors = registrationFailedException.Errors;
            Log.Error($"Failed to register user. Inputted values are not valid: {registrationFailedException.Errors}");
            exceptionContext.Result = new JsonResult(errors)
                {
                    StatusCode = 400
                };
        }
        else if (exceptionContext.Exception is ModelInvalidException modelInvalidException)
        {
            Log.Error($"Failed to register user. Model is invalid: {modelInvalidException.Message}");
            exceptionContext.Result = exceptionContext.Result = new JsonResult(modelInvalidException.Message)
            {
                StatusCode = 400
            };
        }
        else if (exceptionContext.Exception is SqlException sqlException)
        {
            Log.Error($"Failed to register user. Can't produce SQL query: {sqlException.Errors}");
            exceptionContext.Result = new JsonResult(sqlException.Errors)
            {
                StatusCode = 400
            };
        }
        else
        {
            Log.Error($"Failed to register user. Exception is unhandled: {exceptionContext.Exception.Message}");
            exceptionContext.Result = new BadRequestObjectResult(exceptionContext.Exception.Message);
        }
        base.OnException(exceptionContext);
    }
}
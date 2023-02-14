using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace moviesStorage.IdentityService.Filters.LogoutFilters;

public class LogoutErrorFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        Log.Error($"Failed to logout user. Exception is unhandled: {context.Exception.Message}");
        base.OnException(context);
    }
}
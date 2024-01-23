using Microsoft.AspNetCore.Mvc.Filters;

namespace Dem.WebApi.CustomAttributes;

[AttributeUsage(AttributeTargets.Method)]
public class LoggingAttribute(ILogger<LoggingAttribute> logger) : ActionFilterAttribute
{
    //public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //{
    //    //if (!context.ActionDescriptor.FilterDescriptors.Any(i => i.Filter.GetType() == typeof(LoggingAttribute)))
    //    //{
    //    //    await next();
    //    //    return;
    //    //}

    //    var logger = context.HttpContext.RequestServices.GetService<ILogger<LoggingAttribute>>();

    //    // Before
    //    logger.LogInformation("***********Çalışma öncesi log: " + context.HttpContext.Request.Path);

    //    await next();

    //    // After
    //    logger.LogInformation("***********Çalışma sonrası log: " + context.HttpContext.Response.StatusCode);
    //}

    //public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    //{
    //    if (next != null)
    //    {
    //        logger.LogInformation("***********Önce OnResultExecutionAsync log: " + context.HttpContext.Response.StatusCode);
    //        await next();
    //    }
    //    else
    //    {
    //        logger.LogInformation("***********OnResultExecutionAsync log: next değeri null");
    //    }

    //    logger.LogInformation("***********Sonra OnResultExecutionAsync log: " + context.HttpContext.Response.StatusCode);
    //}
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("***********OnActionExecuting log: " + context.HttpContext.Response.StatusCode);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        logger.LogInformation("***********OnResultExecuted log: " + context.HttpContext.Response.StatusCode);
    }
}
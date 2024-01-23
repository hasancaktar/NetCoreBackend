using MediatR;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.ServiceProcess;

namespace Dem.Application.CustomAttributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class LoggingAttribute : Attribute
{
    public string Message { get; }

    public LoggingAttribute(string message)
    {
        Message = message;
    }
}

public class LoggingBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        var loggingAttribute = GetLoggingAttribute(request);

        if (loggingAttribute != null)
        {
            Log($"Entering {request.GetType().Name} - {loggingAttribute.Message}");
        }

        try
        {
            var response = await next();

            if (loggingAttribute != null)
            {
                Log($"Exiting {request.GetType().Name} - {loggingAttribute.Message}");
            }

            return response;
        }
        catch (Exception ex)
        {
            if (loggingAttribute != null)
            {
                Log($"Error in {request.GetType().Name} - {loggingAttribute.Message}: {ex.Message}");
            }

            throw;
        }
    }

    private LoggingAttribute GetLoggingAttribute(TRequest request)
    {
        var attribute = request.GetType().Assembly;
        return null;
    }

    private void Log(string message)
    {
        Console.WriteLine($"[LoggingBehavior] {message}");
    }

    private string GetControllerName()
    {
        var httpContext = httpContextAccessor.HttpContext;
        var controllerName = "";
        return controllerName ?? "UnknownController";
    }
}
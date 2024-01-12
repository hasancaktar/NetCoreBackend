using Dem.Application.Middlewares.Exception.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Dem.Application.Middlewares.Exception;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception exception)
        {
            await ExceptionHandler(context, exception);
        }
    }

    private async Task ExceptionHandler(HttpContext context, System.Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        if (exception is ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var exceptionResponse = new ValidationErrorResponse
            {
                Errors = validationException.Errors.Select(error => error.ErrorMessage).ToList()
            };

            var exceptionJson = JsonConvert.SerializeObject(exceptionResponse, Formatting.Indented);

            _logger.LogError(exception.Message);

            await context.Response.WriteAsync(exceptionJson);
        }

        if (exception is ArgumentException argumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errorResponse = new ArgumentErrorResponse
            {
                Error = argumentException.Message,
            };

            _logger.LogError(exception.Message);
            var responseJson = JsonConvert.SerializeObject(errorResponse, Formatting.Indented);
            await context.Response.WriteAsync(responseJson);
        }
    }
}
using Dem.Application.Middlewares.Exception.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Dem.Application.Middlewares.Exception;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
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
            logger.LogError(exception.InnerException.Message);
            await context.Response.WriteAsync(exceptionJson);
        }

        if (exception is ArgumentException argumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var errorResponse = new ArgumentErrorResponse();

            if (argumentException.InnerException is null)
            {
                errorResponse.Error = argumentException.Message;
                logger.LogError(exception.Message);
                var responseJson = JsonConvert.SerializeObject(errorResponse, Formatting.Indented);
                await context.Response.WriteAsync(responseJson);
            }
            else
            {
                errorResponse.Error = argumentException.InnerException.Message;
                logger.LogError(exception.InnerException.Message);
                var responseJson = JsonConvert.SerializeObject(errorResponse, Formatting.Indented);
                await context.Response.WriteAsync(responseJson);
            }
        }
        else
        {
            var errorResponse = new ErrorResponse
            {
                Error = exception.InnerException.Message,
            };
            logger.LogError(exception.InnerException.Message);
            var responseJson = JsonConvert.SerializeObject(errorResponse);
            await context.Response.WriteAsync(responseJson);
        }
    }
}
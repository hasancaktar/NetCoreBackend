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

            var validationErrorResponse = new ValidationErrorResponse
            {
                Errors = validationException.Errors.Select(error => error.ErrorMessage).ToList()
            };

            if (exception.InnerException is null)
            {
                logger.LogError(exception.Message);
                var responseJson = JsonConvert.SerializeObject(validationErrorResponse, Formatting.Indented);
                await context.Response.WriteAsync(responseJson);
            }
            else
            {
                logger.LogError(exception.InnerException.Message);
                var responseJson = JsonConvert.SerializeObject(validationErrorResponse, Formatting.Indented);
                await context.Response.WriteAsync(responseJson);
            }
            return;
        }

        if (exception is ArgumentException argumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var argumentErrorResponse = new ArgumentErrorResponse();

            if (argumentException.InnerException is null)
            {
                argumentErrorResponse.Error = argumentException.Message;
                logger.LogError(exception.Message);
                var responseJson = JsonConvert.SerializeObject(argumentErrorResponse, Formatting.Indented);
                await context.Response.WriteAsync(responseJson);
            }
            else
            {
                argumentErrorResponse.Error = argumentException.InnerException.Message;
                logger.LogError(exception.InnerException.Message);
                var responseJson = JsonConvert.SerializeObject(argumentErrorResponse, Formatting.Indented);
                await context.Response.WriteAsync(responseJson);
            }
            return;
        }

        var errorResponse = new ErrorResponse
        {
            Error = exception.InnerException.Message,
        };
        if (exception.InnerException is null)
        {
            errorResponse.Error = exception.Message;
            logger.LogError(exception.Message);
            var responseJson = JsonConvert.SerializeObject(errorResponse, Formatting.Indented);
            await context.Response.WriteAsync(responseJson);
        }
        else
        {
            errorResponse.Error = exception.InnerException.Message;
            logger.LogError(exception.InnerException.Message);
            var responseJson = JsonConvert.SerializeObject(errorResponse, Formatting.Indented);
            await context.Response.WriteAsync(responseJson);
        }
    }
}
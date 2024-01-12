namespace Dem.Application.Middlewares.Exception.Models;

public class ValidationErrorResponse : BaseErrorResponse
{
    public List<string>? Errors { get; set; }
}
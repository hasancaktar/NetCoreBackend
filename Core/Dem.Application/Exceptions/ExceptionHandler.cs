namespace Dem.Application.Exceptions;

public class ExceptionHandler : ArgumentException
{
    public ExceptionHandler() : base("Beklenmeyen bir hata alındı!")
    {
    }

    public ExceptionHandler(string? message) : base(message)
    {
    }

    public ExceptionHandler(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
using System.Net;

namespace ALC.WebApp.MVC.Extensions;

public class CustomHttpRequestException : Exception
{
    public HttpStatusCode StatusCode;
    public CustomHttpRequestException() { }

    public CustomHttpRequestException(string? message, Exception? innerException)
         : base(message, innerException) { }

    public CustomHttpRequestException(HttpStatusCode statusCode)
    {
        statusCode = StatusCode;
    }
}
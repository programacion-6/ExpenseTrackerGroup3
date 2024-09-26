using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public abstract class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorCode { get; }
    public string? Details { get; }
    public DateTime TimeStamp { get; }

    protected ApiException(HttpStatusCode statusCode, string message, string errorCode, string? details = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        Details = details;
        TimeStamp = DateTime.Now;
    }
}

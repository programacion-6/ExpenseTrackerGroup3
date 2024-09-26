using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public abstract class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorCode { get; }
    public DateTime TimeStamp { get; }

    protected ApiException(HttpStatusCode statusCode, string message, string errorCode)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        TimeStamp = DateTime.Now;
    }
}

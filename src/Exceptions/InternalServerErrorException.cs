using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public class InternalServerErrorException : ApiException
{
    public InternalServerErrorException(string message, string? details = null)
        : base(HttpStatusCode.InternalServerError, message, "INTERNAL_SERVER_ERROR", details)
    {
    }
}

using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string message, string? details = null)
        : base(HttpStatusCode.BadRequest, message, "BAD_REQUEST", details)
    {
    }
}

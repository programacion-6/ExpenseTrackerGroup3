using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string message)
        : base(HttpStatusCode.BadRequest, message, "BAD_REQUEST")
    {
    }
}

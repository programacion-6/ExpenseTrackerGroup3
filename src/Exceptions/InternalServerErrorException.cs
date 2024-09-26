using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public class InternalServerErrorException : ApiException
{
    public InternalServerErrorException(string message)
        : base(HttpStatusCode.InternalServerError, message, "INTERNAL_SERVER_ERROR")
    {
    }
}

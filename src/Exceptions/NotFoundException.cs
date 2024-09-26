using System.Net;

namespace ExpenseTrackerGroup3.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message, string? details = null)
        : base(HttpStatusCode.NotFound, message, "NOT_FOUND", details)
    {
    }
}

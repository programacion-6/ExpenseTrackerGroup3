using System.Net;

using ExpenseTrackerGroup3.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGroup3.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult HandleException(Exception e)
    {
        HttpStatusCode statusCode;
        string message;

        switch (e)
        {
            case ArgumentException argumentException:
                statusCode = HttpStatusCode.BadRequest;
                message = argumentException.Message;
                break;
            
            case Exception exception:
                statusCode = HttpStatusCode.NotAcceptable;
                message = exception.Message;
                break;

            default:
            statusCode = HttpStatusCode.InternalServerError;
            message = "An unexpected error occurred.";
            break;
        }

        throw new BusinessException(statusCode, message);
    }
}

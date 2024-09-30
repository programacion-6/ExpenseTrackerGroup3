using System.Net;
using System.Text.Json;

using ExpenseTrackerGroup3.Exceptions;
using FluentValidation;

namespace ExpenseTrackerGroup3.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            _logger.LogError($"Something went wrong: {e}");
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware."
        };

        switch (exception)
        {
             case ValidationException validationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Validation error";
                response.ErrorCode = "VALIDATION_ERROR";
                response.Details = validationException.Errors.Select(e => new ValidationError
                {
                    PropertyName = e.PropertyName,
                    ErrorMessage = e.ErrorMessage
                }).ToList();
                break;
            case ApiException apiException:
                response.StatusCode = (int)apiException.StatusCode;
                response.Message = apiException.Message;
                response.ErrorCode = apiException.ErrorCode;
                response.TimeStamp = apiException.TimeStamp;
                response.Details = [];
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Message = "An unexpected error occurred.";
                response.ErrorCode = "UNEXPECTED_ERROR";
                response.TimeStamp = DateTime.Now;
                response.Details = [];
                break;
        }

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

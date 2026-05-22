using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Web.Host.ExceptionHandler;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        int statusCode;
        object result;

        switch (exception)
        {
           
            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;

                var errors = validationException.Errors
                    .Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    });

                result = new
                {
                    success = false,
                    message = "Validation Failed",
                    errors
                };
                break;

            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                result = new
                {
                    success = false,
                    message = exception.Message
                };
                break;

    
            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                result = new
                {
                    success = false,
                    message = exception.Message
                };
                break;

       
            case ArgumentException:
                statusCode = (int)HttpStatusCode.BadRequest;
                result = new
                {
                    success = false,
                    message = exception.Message
                };
                break;

     
            case DbUpdateException:
                statusCode = (int)HttpStatusCode.BadRequest;
                result = new
                {
                    success = false,
                    message = "Database error occurred"
                };
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;

#if DEBUG
                var message = exception.Message;
#else
                var message = "Something went wrong";
#endif

                result = new
                {
                    success = false,
                    message = message
                };
                break;
        }

        response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(result);

        await response.WriteAsync(json);
    }
}
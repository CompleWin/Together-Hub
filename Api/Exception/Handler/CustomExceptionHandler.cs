using Application.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Exception.Handler;

public class CustomExceptionHandler(
    ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken ct)
    {
        logger.LogWarning("Handling exception: ({message}), time: ({time})",
            exception.Message, DateTime.Now);

        (string Detail, string Title, int StatusCode) exceptionDetails = exception switch
        {
            NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound
            ),

            UserWrongEmailOrPasswordException => (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            
            
            UserException => (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized
            ),


            _ => (
                exception.Message,
                exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        ProblemDetails problemDetails = new()
        {
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail,
            Status = exceptionDetails.StatusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, ct);

        return true;
    }
}
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FinalLabTask1.Middleware;

public class GlobalExceptionHandler:IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }


    public async  ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        
        _logger.LogError($"An error has occured while sending your request.\n{exception.Message}");
        var statusCode = httpContext.Response.StatusCode;

        switch (exception)
        {
            case Exception e:
                _logger.LogError($"An error has occured while sending your request.\n{e.Message}"); break;
            default:
                statusCode = 500; break;//internal server error
                
                
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An error occured.",
            Detail = exception.Message,
            Type = exception.GetType().FullName
            
        };
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        //async-await to not block the request thread
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
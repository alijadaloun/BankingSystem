using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalLabTask1.Filters;

public class LoggingActionFilter: IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation(context.ActionDescriptor.DisplayName);
        _logger.LogInformation($"Transaction sent at: {DateTime.UtcNow}");
        // _logger.LogInformation(context.Result.ToString()); //null reference exception
        
    }

    public void OnActionExecuted( ActionExecutedContext context )
    {
        _logger.LogInformation(context.ActionDescriptor.DisplayName);
        _logger.LogInformation($"Transaction received at: {DateTime.UtcNow}");
        _logger.LogInformation(context.HttpContext.Response.StatusCode.ToString());
        _logger.LogInformation(context.HttpContext.Response.Body.ToString());
        
        
        
    }
}
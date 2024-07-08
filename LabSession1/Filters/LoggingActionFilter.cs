using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LabSession1.Filters;

public class LoggingActionFilter : IActionFilter
{
    private readonly ILogger<LoggingActionFilter> _logger;

    public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Action {ActionName} started at {StartTime}",
            context.ActionDescriptor.DisplayName, DateTime.UtcNow);

        // log parameters
        foreach (var param in context.ActionArguments)
        {
            _logger.LogInformation("Parameter {ParamName}: {ParamValue}", param.Key, param.Value);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action {ActionName} ended at {EndTime}",
            context.ActionDescriptor.DisplayName, DateTime.UtcNow);

        // Log result
        if (context.Result is ObjectResult objectResult)
        {
            _logger.LogInformation("Result: {StatusCode}, {Value}", objectResult.StatusCode, objectResult.Value);
        }
        else if (context.Result is StatusCodeResult statusCodeResult)
        {
            _logger.LogInformation("Result: {StatusCode}", statusCodeResult.StatusCode);
        }
    }
}
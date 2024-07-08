namespace LabSession1.Middlewares;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    // dependancy injection for ILogger in constructor because ILogger is singleton
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var method = request.Method;
        var path = request.Path;
        var queryString = request.QueryString.ToString();
        var headers = request.Headers;

        // Log Method, Path, and Query String
        _logger.LogInformation($"Request {method} {path}{queryString}");

        // Log Headers
        foreach (var header in headers)
        {
            _logger.LogInformation($"Header: {header.Key}={header.Value}");
        }

        // Log Request Body
        if (request.ContentLength > 0 && request.ContentType != null && request.ContentType.Contains("application/json"))
        {
            request.EnableBuffering(); //  By default, the request body can only be read once. Buffering allows the body to be read multiple times
            var bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();
            _logger.LogInformation($"Request Body: {bodyAsText}");
            request.Body.Position = 0; // for next middleware to read from begining
        }

        // Log Timestamp
        var timestamp = DateTime.Now;
        _logger.LogInformation($"Timestamp: {timestamp}");

        // Copy original response stream to capture response body
        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream(); // to ditruct it when no longer in use
        context.Response.Body = responseBodyStream;

        await _next(context);

        // Log Response Status Code
        var statusCode = context.Response.StatusCode;
        _logger.LogInformation($"Response Status Code: {statusCode}");

        // Copy response body back to the original stream
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        await responseBodyStream.CopyToAsync(originalBodyStream);
        context.Response.Body = originalBodyStream;
    }
}


// to use it in program.cs in a proper way
public static class RequestLoggingMiddlewareExtension
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
using System.Diagnostics;

namespace backend.Helpers;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Place your breakpoint here to inspect the incoming request
        Debug.WriteLine($"Request Path: {context.Request.Path}");

        // Call the next middleware in the pipeline
        await _next(context);
    }
}

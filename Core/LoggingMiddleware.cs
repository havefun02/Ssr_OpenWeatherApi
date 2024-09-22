using System.Diagnostics;
namespace App.Core
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);
            await _next(context);
            watch.Stop();
            var elapsedMilliseconds = watch.ElapsedMilliseconds;
            _logger.LogInformation("Finished handling request: {Method} {Path} with status code {StatusCode} in {ElapsedMilliseconds}ms",
                context.Request.Method, context.Request.Path, context.Response.StatusCode, elapsedMilliseconds);
        }
    }
}

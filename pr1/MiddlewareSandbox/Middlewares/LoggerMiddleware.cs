using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MiddlewareSandbox.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string requestMethod = context.Request.Method;
            string requestPath = context.Request.Path;

            _logger.LogInformation("Request received: {Method} {Path}", requestMethod, requestPath);

            await _next(context);
        }
    }

    public static class LoggerMiddlewareExtensions
    {
        public static IApplicationBuilder Logger(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggerMiddleware>();
        }
    }
}

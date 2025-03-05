using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewareSandbox.Middlewares
{
    public class ApiMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string Key = "my-key";

        public ApiMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) || extractedApiKey != Key)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Forbidden: Invalid or missing API key.");
                return;
            }

            await _next(context);
        }
    }

    public static class ApiMiddlewareExtensions
    {
        public static IApplicationBuilder UseApi(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiMiddleware>();
        }
    }
}

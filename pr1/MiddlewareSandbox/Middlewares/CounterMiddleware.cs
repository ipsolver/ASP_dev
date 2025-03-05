using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewareSandbox.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CounterMiddleware
    {
        private readonly RequestDelegate _next;
        private static int counter = 0;

        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            counter++;
           // context.Response.Headers.Add("X-Request-Count", counter.ToString());

            if (context.Request.Path == "/count")
            {
                await context.Response.WriteAsync($"The amount of processed requests is {counter}");
                return;
            }

            await _next(context);
        }
    }

    public static class CounterMiddlewareExtensions
    {
        public static IApplicationBuilder MyCounter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CounterMiddleware>();
        }
    }
}

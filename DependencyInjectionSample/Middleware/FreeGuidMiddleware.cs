using System;
using System.Threading.Tasks;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Http;

namespace DependencyInjectionSample.Middleware
{
    public class FreeGuidMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GuidGenerator _generator;

        public FreeGuidMiddleware(RequestDelegate next, GuidGenerator generator)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-RequestFreeGuid", out var correlationId))
            {
           
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-FreeGUID", _generator.Generate());
                    return Task.CompletedTask;
                });
            }

            return _next(context);
        }
    }
}
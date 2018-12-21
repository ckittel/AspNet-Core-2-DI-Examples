using Microsoft.AspNetCore.Builder;

namespace DependencyInjectionSample.Middleware
{
    public static class FreeGuidMiddlewareExtensions
    {
        public static IApplicationBuilder UseFreeGuidMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FreeGuidMiddleware>();
        }
    }
}
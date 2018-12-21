using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjectionSample.Middleware;

namespace DependencyInjectionSample
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // As your application grows in size, the number of DI wire-ups can get large, first
            // tip is to hide all of that away just like AddMvc() is doing above.
            services.AddInternalServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseFreeGuidMiddleware();
            app.UseMvc();
        }
    }
}

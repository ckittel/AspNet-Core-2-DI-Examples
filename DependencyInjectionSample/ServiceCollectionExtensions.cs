using System;
using DependencyInjectionSample.Filters;
using DependencyInjectionSample.HttpClients;
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Models;
using DependencyInjectionSample.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionSample
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            // Transient - Every time an IOperationTransient is needed, a new one will be created
            services.AddTransient<IOperationTransient, Operation>();


            // Singleton - The first time an IOperationSingleton is needed, a new on will be
            //             created.  Future needs will be handled by the same instance.  This
            //             is a singleton for the life of the application.
            services.AddSingleton<IOperationSingleton, Operation>();


            // Scoped - The first time an IOperationScoped is needed within an http request, a new
            //          one will be created.  Future needs will be handed the same instance.  Can
            //          think of this as a "singleton within the scope of an HttpRequest
            services.AddScoped<IOperationScoped, Operation>();


            // Try - All of the above have a "Try" version that only adds a mapping if one doesn't
            //       already exist
            // services.TryAddSingleton<IOperationTransient, Operation>();


            // Concrete - You don't need to have an interface for IoC.  This is an example
            //            of class OperationService being made available via IoC
            // Composing - This is also an example of transparent composition.  The ctor for
            //             this class depends on the various IOperation* as created above
            //             Tip: When possible, order your configuration logically like this.
            services.AddTransient<OperationService>();


            // All of the above examples include ctors that have no arguments or where the args
            // are populated with IoC.  The next two examples are showing how to explicitly define
            // the instance/construction process. -- Connection string is a good example

            // The "Create the instance now" approach
            var provider = services.BuildServiceProvider();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(provider.GetRequiredService<ILogger<Operation>>(), Guid.Empty));

            // The "Create the instance when asked/needed" approach
            //services.AddSingleton<IOperationSingletonInstance>(provider => new Operation(provider.GetRequiredService<ILogger<Operation>>(), Guid.Empty));


            // Reminder that all IDisposable will be disposed at the end of the lifecycle


            // Example of a /Scoped/ that uses a /Transient/ dependency - BAD NEWS
            // Note if this would have been AddSingleton -- Exception would have been thrown -- AT RUNTIME
            // Demo Note: Comment in the return object in ValuesController
            // services.AddScoped<OperationService>();


            // A detour to talk about HttpClient, DocumentClient (CosmosDB), and Azure Storage Clients
            // HttpClientFactory - New as of 2.1
            // HttpClient doesn't respect DNS changes, its designed to be thread-safe, expensive to request -- socket exhaustion, etc

            // Advanced usages: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
            // More samples: https://github.com/aspnet/Docs/blob/master/aspnetcore/fundamentals/http-requests/samples/2.x/HttpClientFactorySample/Startup.cs
            // Including the usage of polly
            services.AddHttpClient();

            // Registers this as transient and passes it in
            services.AddHttpClient<IIPLookupHttpClient, IPLookupHttpClient>(c =>
            {
                c.BaseAddress = new Uri("https://ifconfig.co/");
                c.DefaultRequestHeaders.Add("User-Agent", "NinjaCat/9001.42");
                c.Timeout = TimeSpan.FromSeconds(1);
            });

            // Also shout out for Refit -- great rest library that can also take advantage of these HttpClientFactory enhancements.

            // Sometimes Transient isn't... specifically in Middleware.  ASP.NET Core Middleware
            // is instantiated once for the life of the app. Check out UseFreeGuidMiddleware
            services.AddTransient<GuidGenerator>();

            // Action Filters and three DI options - AddGuidHeader + ValuesController
            // services.AddSingleton<GuidGenerator>();

            //services.AddScoped<AddGuidHeaderAttribute>();
            // services.AddSingleton<AddGuidHeaderAttribute>();  // Look out for a gotcha with services.AddTransient<GuidGenerator>();

            return services;


            // You can break AddInternalServices up even more, taking each logical grouping above and breaking them out even further
            // It's not "bad" that this area is digging into various projects you have referenced and wiring things up.  This layer of
            // the application is called the composition root




            // Topic not demoed was injecting into Views.  Generally speaking data should be coming from the controller, but
            // its still possible.  You can use @inject
            //
            // E.g.
            //  @using DependencyInjectionSample.Models
            //  @using DependencyInjectionSample.Services
            //  @model IEnumerable<MyPageModelItem>
            //  @inject GuidGenerator GuidGen
            //  ...
            //  <li>Your Guid: @GuidGen.Generate()</li>
        }
    }
}
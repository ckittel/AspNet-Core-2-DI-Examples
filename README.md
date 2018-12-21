# ASP.NET Core 2.2 Dependency Injection (DI) Examples

This is a small ASP.NET Core project that demos a few DI concepts.

It touches on:
* Scopes (Transient, Singleton, Scoped)
* `Add` vs `TryAdd`
* `ServiceCollection` extension to keep the `ConfigureServices()` method cleaner
* Composition of dependencies
* Control over the instance construction
* Note about IDisposable (not demoed)
* `HttpClient`, `HttpClientFactory`, `DocumentClient`, etc
* Middleware vs Transient scope
* Action Filters
* * `ServiceFilterAttribute`
* * two variations of `TypeFilterAttribute`
* View injection (not demoed)
* Unit & Integration Testing

Learn more about DI in ASP.NET Core @ https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection

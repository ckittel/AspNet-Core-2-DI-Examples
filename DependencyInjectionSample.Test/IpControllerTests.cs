using System.Net.Http;
using System.Threading.Tasks;
using DependencyInjectionSample.Controllers;
using DependencyInjectionSample.HttpClients;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DependencyInjectionSample.Test
{
    [TestClass]
    public class IpControllerTests
    {
        [TestMethod]
        public async Task Get_When_IPLookupReturnsValue_Then_SameValueReturnFromController()
        {
            // Arrange
            const string theServiceReturnValue = "This is whatever the service returns";
            var ipLookupClient = new Mock<IIPLookupHttpClient>();
            ipLookupClient.Setup(c => c.GetAsync()).ReturnsAsync(theServiceReturnValue);
            var controller = new IpController(Mock.Of<IHttpClientFactory>(), ipLookupClient.Object);

            // Act
            var response = await controller.Get();

            // Assert
            var actionResult = response.Should().BeOfType<ActionResult<string>>().Subject;
            var okObjectResult = actionResult.Result.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeOfType<string>()
                          .Which.Should().Be(theServiceReturnValue);
        }

        [TestMethod]
        public async Task Integration_Get_When_IPLookupReturnsValue_Then_SameValueReturnFromController()
        {
            // Arrange
            const string theServiceReturnValue = "This is whatever the service returns";
            var ipLookupClient = new Mock<IIPLookupHttpClient>();
            ipLookupClient.Setup(c => c.GetAsync()).ReturnsAsync(theServiceReturnValue);
            var appFactory = new WebApplicationFactory<Startup>()
                                 .WithWebHostBuilder(c =>
                                 {
                                     c.ConfigureTestServices(services =>
                                     {
                                         services.AddTransient(s => ipLookupClient.Object);
                                         services.AddTransient(s => Mock.Of<IHttpClientFactory>());
                                     });
                                 });
            var appClient = appFactory.CreateClient();

            // Act
            var response = await appClient.GetStringAsync("/api/ip");

            // Assert
            response.Should().Be(theServiceReturnValue);
        }
    }
}

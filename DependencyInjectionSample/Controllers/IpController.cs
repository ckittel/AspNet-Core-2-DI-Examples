using System;
using System.Net.Http;
using System.Threading.Tasks;
using DependencyInjectionSample.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIPLookupHttpClient _ipLookupClient;

        public IpController(IHttpClientFactory httpClientFactory, IIPLookupHttpClient ipLookupClient)
        {
            _httpClientFactory = httpClientFactory;
            _ipLookupClient = ipLookupClient;
        }
        // GET api/http
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            string result;

            // NO, No, no
            //using (var httpClient = new HttpClient())
            //{
            //    result = await httpClient.GetStringAsync("https://ifconfig.co/json");
            //}

            // Better
            //using (var httpClient = _httpClientFactory.CreateClient())
            //{
            //    result = await httpClient.GetStringAsync("https://ifconfig.co/json");
            //}

            // Even Better
            result = await _ipLookupClient.GetAsync();

            return Ok(result);
        }
    }
}
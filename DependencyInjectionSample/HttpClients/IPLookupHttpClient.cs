using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DependencyInjectionSample.HttpClients
{
    public class IPLookupHttpClient : IIPLookupHttpClient
    {
        private readonly HttpClient _httpClient;
        public IPLookupHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync()
        {
            return await _httpClient.GetStringAsync("json");
        }
    }
}
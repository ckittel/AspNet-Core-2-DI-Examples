using System.Threading.Tasks;

namespace DependencyInjectionSample.HttpClients
{
    public interface IIPLookupHttpClient
    {
        Task<string> GetAsync();
    }
}
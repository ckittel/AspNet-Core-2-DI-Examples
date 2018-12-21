using System;

namespace DependencyInjectionSample.Services
{
    public class GuidGenerator
    {
        private readonly string _storedGuid;

        public GuidGenerator()
        {
            _storedGuid = Guid.NewGuid().ToString();
        }
        public string Generate()
        {
            return _storedGuid;
        }
    }
}

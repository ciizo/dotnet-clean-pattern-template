using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Ciizo.Restful.Onion.IntegrationTests
{
    public class TestFixture
    {
        private readonly WebApplicationFactory<Program> _factory;

        private readonly IServiceScopeFactory _scopeFactory;

        public TestFixture()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        public HttpClient GetHttpClient()
        {
            return _factory.CreateClient();
        }

        public IServiceScope CreateScope()
        {
            return _scopeFactory.CreateScope();
        }
    }
}
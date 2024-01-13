using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Ciizo.CleanPattern.IntegrationTests
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
            var _httpClient = _factory.CreateClient();
            var token = JwtTokenGenerator.GenerateJwtToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return _httpClient;
        }

        public IServiceScope CreateScope()
        {
            return _scopeFactory.CreateScope();
        }
    }
}
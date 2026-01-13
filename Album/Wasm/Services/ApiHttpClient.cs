using System.Net.Http;

namespace Wasm.Services
{
    public class ApiHttpClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly ApiEndpointService _api;

        public ApiHttpClient(IHttpClientFactory factory, ApiEndpointService api)
        {
            _factory = factory;
            _api = api;
        }

        // ---------------------------------------------------------
        // AUTHENTICATED CLIENT (uses TokenHttpHandler)
        // ---------------------------------------------------------
        public HttpClient CreateAuthenticated()
        {
            return _factory.CreateClient(_api.CurrentClientName);
        }

        // ---------------------------------------------------------
        // UNAUTHENTICATED CLIENT (login, refresh, logout)
        // ---------------------------------------------------------
        public HttpClient CreateUnauthenticated()
        {
            return _factory.CreateClient(_api.CurrentClientName); // "AuthApi");
        }
    }
}
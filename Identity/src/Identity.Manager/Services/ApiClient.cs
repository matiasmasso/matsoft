using System.Net.Http.Headers;

namespace Identity.Manager.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly PKCEAuthService _authService;

        public ApiClient(HttpClient httpClient, PKCEAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<HttpResponseMessage> GetSecureDataAsync()
        {
            var token = await _authService.GetAccessTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetAsync("https://localhost:7105/api/secure-endpoint");
        }
    }
}

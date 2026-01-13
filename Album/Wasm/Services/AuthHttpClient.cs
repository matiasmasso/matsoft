using System.Net.Http.Headers;
using System.Net.Http.Json;
using Wasm.Services;

public sealed class AuthHttpClient
{
    private readonly HttpClient _http;
    private readonly ITokenStore _tokenStore;

    public AuthHttpClient(HttpClient http, ITokenStore tokenStore)
    {
        _http = http;
        _tokenStore = tokenStore;
    }

    private async Task AddAuthHeaderAsync(HttpRequestMessage request)
    {
        var bundle = await _tokenStore.GetBundleAsync();
        if (bundle is null)
            return;

        if (!string.IsNullOrWhiteSpace(bundle.AccessToken))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", bundle.AccessToken);
        }
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        await AddAuthHeaderAsync(request);
        return await _http.SendAsync(request);
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, url);
        var res = await SendAsync(req);

        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<T>();
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest payload)
    {
        var req = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = JsonContent.Create(payload)
        };

        var res = await SendAsync(req);

        res.EnsureSuccessStatusCode();
        return await res.Content.ReadFromJsonAsync<TResponse>();
    }
}
using System.Net.Http.Json;

namespace Album.Client.Http;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    // -----------------------------
    // GET (typed)
    // -----------------------------
    public async Task<T?> GetAsync<T>(string url)
    {
        return await _http.GetFromJsonAsync<T>(url);
    }

    // -----------------------------
    // POST (typed response)
    // -----------------------------
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body)
    {
        var response = await _http.PostAsJsonAsync(url, body);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    // -----------------------------
    // POST (no response body)
    // -----------------------------
    public async Task<bool> PostAsync<TRequest>(string url, TRequest body)
    {
        var response = await _http.PostAsJsonAsync(url, body);
        return response.IsSuccessStatusCode;
    }

    // -----------------------------
    // POST (empty body)
    // -----------------------------
    public async Task<bool> PostAsync(string url)
    {
        var response = await _http.PostAsync(url, null);
        return response.IsSuccessStatusCode;
    }

    // -----------------------------
    // PUT
    // -----------------------------
    public async Task<bool> PutAsync<TRequest>(string url, TRequest body)
    {
        var response = await _http.PutAsJsonAsync(url, body);
        return response.IsSuccessStatusCode;
    }

    // -----------------------------
    // DELETE
    // -----------------------------
    public async Task<bool> DeleteAsync(string url)
    {
        var response = await _http.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }
}
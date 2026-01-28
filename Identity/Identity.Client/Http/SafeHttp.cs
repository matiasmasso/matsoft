using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Identity.Client.Http;

public class SafeHttp
{
    private readonly HttpClient _http;

    public SafeHttp(HttpClient http)
    {
        _http = http;
    }

    public async Task<Result<T>> Get<T>(string url)
    {
        try
        {
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return Result<T>.Fail(await response.Content.ReadAsStringAsync());

            var data = await response.Content.ReadFromJsonAsync<T>();
            return Result<T>.Ok(data!);
        }
        catch (Exception ex)
        {
            return Result<T>.Fail(ex.Message);
        }
    }

    public async Task<Result<T>> Post<T>(string url, object body)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(url, body);

            if (!response.IsSuccessStatusCode)
                return Result<T>.Fail(await response.Content.ReadAsStringAsync());

            var data = await response.Content.ReadFromJsonAsync<T>();
            return Result<T>.Ok(data!);
        }
        catch (Exception ex)
        {
            return Result<T>.Fail(ex.Message);
        }
    }
}
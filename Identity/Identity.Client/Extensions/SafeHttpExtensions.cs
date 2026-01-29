using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public static class SafeHttpExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task<T> SafeGetAsync<T>(this HttpClient http, string url)
    {
        var response = await http.GetAsync(url);
        return await HandleResponse<T>(response);
    }

    public static async Task<T> SafePostAsync<T>(this HttpClient http, string url, object body)
    {
        var response = await http.PostAsJsonAsync(url, body);
        return await HandleResponse<T>(response);
    }

    public static async Task SafePostAsync(this HttpClient http, string url, object body)
    {
        var response = await http.PostAsJsonAsync(url, body);
        await HandleResponse(response);
    }

    public static async Task<T> SafePutAsync<T>(this HttpClient http, string url, object body)
    {
        var response = await http.PutAsJsonAsync(url, body);
        return await HandleResponse<T>(response);
    }

    public static async Task SafePutAsync(this HttpClient http, string url, object body)
    {
        var response = await http.PutAsJsonAsync(url, body);
        await HandleResponse(response);
    }

    public static async Task SafeDeleteAsync(this HttpClient http, string url)
    {
        var response = await http.DeleteAsync(url);
        await HandleResponse(response);
    }

    // -------------------------
    // Internal helpers
    // -------------------------

    private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        var raw = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new SafeHttpException(raw);

        return JsonSerializer.Deserialize<T>(raw, JsonOptions)!;
    }

    private static async Task HandleResponse(HttpResponseMessage response)
    {
        var raw = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new SafeHttpException(raw);
    }
}
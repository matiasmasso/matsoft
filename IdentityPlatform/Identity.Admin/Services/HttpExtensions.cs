using System.Net.Http.Json;
using System.Text.Json;
using Identity.Admin.Models;

namespace Identity.Admin.Services;

public static class HttpExtensions
{
    public static async Task<T?> ReadOrThrowAsync<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                var message = errorObj?["error"] ?? json;
                throw new ApiException(message, (int)response.StatusCode);
            }
            catch
            {
                throw new ApiException(json, (int)response.StatusCode);
            }
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public static async Task ThrowIfErrorAsync(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                var message = errorObj?["error"] ?? json;
                throw new ApiException(message, (int)response.StatusCode);
            }
            catch
            {
                throw new ApiException(json, (int)response.StatusCode);
            }
        }
    }
}
using Identity.Admin.Models;
using System.Net.Http.Json;

namespace Identity.Admin.Http;

public static class HttpExtensions
{
    public static async Task<T?> ReadOrThrowAsync<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var message = await ErrorFormatting.BuildErrorMessage(response);
            throw new ApiException(message, (int)response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public static async Task ThrowIfErrorAsync(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var message = await ErrorFormatting.BuildErrorMessage(response);
            throw new ApiException(message, (int)response.StatusCode);
        }
    }
}
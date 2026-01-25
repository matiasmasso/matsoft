using System.Text.Json;

namespace ApiCore.Http;

public static class ErrorFormatting
{
    public static async Task<string> BuildErrorMessage(HttpResponseMessage response)
    {
        var url = response.RequestMessage?.RequestUri?.ToString() ?? "(unknown url)";
        var status = (int)response.StatusCode;

        if (url.Length > 120)
            url = url[..120] + "...";

        var json = await response.Content.ReadAsStringAsync();

        string message;

        try
        {
            var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            message = errorObj?["error"] ?? json;
        }
        catch
        {
            message = json;
        }

        return
$"""
{message}

URL: {url}
Status: {status}
""";
    }
}

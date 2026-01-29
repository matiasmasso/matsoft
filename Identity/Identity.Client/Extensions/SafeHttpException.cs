using System;
using System.Text.Json;

public sealed class SafeHttpException : Exception
{
    public string Raw { get; }
    public NormalizedError Normalized { get; }

    public SafeHttpException(string raw)
        : base("HTTP request failed")
    {
        Raw = raw;

        try
        {
            Normalized = JsonSerializer.Deserialize<NormalizedError>(raw,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new NormalizedError { Message = raw };
        }
        catch
        {
            Normalized = new NormalizedError { Message = raw };
        }
    }
}

public sealed class NormalizedError
{
    public string Error { get; set; } = "";
    public string Message { get; set; } = "";
}
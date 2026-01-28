using System.Net.Http.Json;

namespace Identity.Admin.Services
{
    public abstract class _BaseApiClient
    {
        protected readonly HttpClient _http;

        protected _BaseApiClient(HttpClient http)
        {
            _http = http;
        }

        //Success means no return errors.
        protected async Task<List<string>?> CallAsync(
            Func<Task<HttpResponseMessage>> httpCall)
        {
            var response = await httpCall();

            if (response.IsSuccessStatusCode) return null;

            try
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return payload?.Errors ?? new List<string> { "Unknown error" };
            }
            catch
            {
                var raw = await response.Content.ReadAsStringAsync();
                return new List<string> { $"Unexpected error format: {raw}" };
            }
        }

        //Success means I return data.
        protected async Task<(T? Data, List<string>? Errors)> ReadAsync<T>(
            Func<Task<HttpResponseMessage>> httpCall)
        {
            var response = await httpCall();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();
                return (data, null);
            }

            try
            {
                var payload = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                return (default, payload?.Errors ?? new List<string> { "Unknown error" });
            }
            catch
            {
                var raw = await response.Content.ReadAsStringAsync();
                return (default, new List<string> { $"Unexpected error format: {raw}" });
            }
        }

    }

    public sealed class ApiResult<T>
    {
        public T? Data { get; init; }
        public List<string>? Errors { get; init; }

        public bool IsSuccess => Errors is null || Errors.Count == 0;

        public static ApiResult<T> Success(T data) =>
            new ApiResult<T> { Data = data };

        public static ApiResult<T> Failure(List<string> errors) =>
            new ApiResult<T> { Errors = errors };
    }

    public class ErrorResponse
    {
        public List<string> Errors { get; set; } = new();
    }

}

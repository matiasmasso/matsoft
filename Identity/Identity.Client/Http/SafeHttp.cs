using Identity.Client.Models;
using Identity.Client.Notifications;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Identity.Client.Http;

public sealed class SafeHttp
{
    private readonly HttpClient _http;
    private readonly IErrorNotifier _notifier;

    public SafeHttp(HttpClient http, IErrorNotifier notifier)
    {
        _http = http;
        _notifier = notifier;
    }

    // -------------------------
    // GET
    // -------------------------
    public async Task<Result<T>> Get<T>(string url)
    {
        try
        {
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return await FailFromResponse<T>(response);

            var data = await response.Content.ReadFromJsonAsync<T>();
            return Result<T>.Ok(data!);
        }
        catch (Exception ex)
        {
            _notifier.NotifyError($"Unexpected error: {ex.Message}");
            return Result<T>.Fail(ex.Message);
        }
    }

    // -------------------------
    // POST
    // -------------------------
    public async Task<Result<T>> Post<T>(string url, object body, string? successMessage = null)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(url, body);

            if (!response.IsSuccessStatusCode)
                return await FailFromResponse<T>(response);

            var data = await response.Content.ReadFromJsonAsync<T>();

            if (!string.IsNullOrWhiteSpace(successMessage))
                _notifier.NotifySuccess(successMessage);

            return Result<T>.Ok(data!);
        }
        catch (Exception ex)
        {
            _notifier.NotifyError($"Unexpected error: {ex.Message}");
            return Result<T>.Fail(ex.Message);
        }
    }

    // -------------------------
    // PUT
    // -------------------------
    public async Task<Result<T>> Put<T>(string url, object body, string? successMessage = null)
    {
        try
        {
            var response = await _http.PutAsJsonAsync(url, body);

            if (!response.IsSuccessStatusCode)
                return await FailFromResponse<T>(response);

            var data = await response.Content.ReadFromJsonAsync<T>();

            if (!string.IsNullOrWhiteSpace(successMessage))
                _notifier.NotifySuccess(successMessage);

            return Result<T>.Ok(data!);
        }
        catch (Exception ex)
        {
            _notifier.NotifyError($"Unexpected error: {ex.Message}");
            return Result<T>.Fail(ex.Message);
        }
    }

    // -------------------------
    // DELETE
    // -------------------------
    public async Task<Result<bool>> Delete(string url, string? successMessage = null)
    {
        try
        {
            var response = await _http.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
                return await FailFromResponse<bool>(response);

            if (!string.IsNullOrWhiteSpace(successMessage))
                _notifier.NotifySuccess(successMessage);

            return Result<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            _notifier.NotifyError($"Unexpected error: {ex.Message}");
            return Result<bool>.Fail(ex.Message);
        }
    }

    // -------------------------
    // Error normalization
    // -------------------------
    private async Task<Result<T>> FailFromResponse<T>(HttpResponseMessage response)
    {
        ApiError? apiError = null;

        try
        {
            apiError = await response.Content.ReadFromJsonAsync<ApiError>();
        }
        catch
        {
            var raw = await response.Content.ReadAsStringAsync();
            _notifier.NotifyError($"HTTP {response.StatusCode}");
            return Result<T>.Fail(raw);
        }

        if (apiError is null)
        {
            var raw = await response.Content.ReadAsStringAsync();
            _notifier.NotifyError($"HTTP {response.StatusCode}");
            return Result<T>.Fail(raw);
        }

        var msg = apiError.Message;

        if (!string.IsNullOrWhiteSpace(apiError.CorrelationId))
            msg += $" (ID: {apiError.CorrelationId})";

        _notifier.NotifyError(msg);

        return Result<T>.Fail(apiError.Message);
    }
}

using Identity.Admin.Components.Shared;

namespace Identity.Admin.Services;

public sealed class ModalService
{
    public event Func<ModalRequest, Task>? OnShow;
    public event Func<Task>? OnClose;

    public TaskCompletionSource<object?>? _tcs;

    public Task<TResult?> OpenAsync<TComponent, TResult>(string title, Dictionary<string, object>? parameters = null)
    {
        _tcs = new TaskCompletionSource<object?>();

        var request = new ModalRequest
        {
            Title = title,
            Component = typeof(TComponent),
            Parameters = parameters ?? new()
        };

        OnShow?.Invoke(request);

        return _tcs.Task.ContinueWith(t => (TResult?)t.Result);
    }

    public Task<bool?> OpenAsync<T>(string title, object parameters)
    {
        var dict = parameters.GetType()
            .GetProperties()
            .ToDictionary(p => p.Name, p => p.GetValue(parameters)!);

        return OpenAsync<bool?>(title, dict);
    }

    public void Close(object? result = null)
    {
        _tcs?.SetResult(result);
        OnClose?.Invoke();
    }

    public Task<bool> ConfirmAsync(string message, string title = "Confirm")
    {
        return OpenAsync<ConfirmDialog, bool>(
            title,
            new Dictionary<string, object>
            {
                ["Message"] = message
            }
        )!;
    }
}

public sealed class ModalRequest
{
    public string Title { get; set; } = default!;
    public Type Component { get; set; } = default!;
    public Dictionary<string, object> Parameters { get; set; } = new();
}
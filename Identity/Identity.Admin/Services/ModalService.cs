using Identity.Admin.Components.Shared;
using Identity.Admin.Services;
using Microsoft.AspNetCore.Components;

public sealed class ModalService
{
    public event Func<ModalRequest, Task>? OnShow;
    public event Func<Task>? OnClose;

    private TaskCompletionSource<object?>? _tcs;

    public Task<TResult?> OpenAsync<TComponent, TResult>(
        string title,
        IReadOnlyDictionary<string, object>? parameters = null
    )
        where TComponent : IComponent
    {
        _tcs = new TaskCompletionSource<object?>();

        var request = new ModalRequest
        {
            Title = title,
            Component = typeof(TComponent),
            Parameters = parameters ?? new Dictionary<string, object>()
        };

        OnShow?.Invoke(request);

        return _tcs.Task.ContinueWith(t => (TResult?)t.Result);
    }

    public Task<bool?> OpenAsync<TComponent>(
        string title,
        IReadOnlyDictionary<string, object>? parameters = null
    )
        where TComponent : IComponent
        => OpenAsync<TComponent, bool?>(title, parameters);

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
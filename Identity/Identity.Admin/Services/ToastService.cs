using Identity.Admin.Components.Shared.Toast;

public sealed class ToastService
{
    public event Action? OnChange;

    public List<ToastMessageModel> Messages { get; } = new();

    public void ShowError(string message)
        => Add(message, ToastType.Error);

    public void ShowSuccess(string message)
        => Add(message, ToastType.Success);

    private void Add(string text, ToastType type)
    {
        var msg = new ToastMessageModel(text, type, Remove);
        Messages.Add(msg);
        OnChange?.Invoke();
    }

    private void Remove(ToastMessageModel msg)
    {
        Messages.Remove(msg);
        OnChange?.Invoke();
    }
}


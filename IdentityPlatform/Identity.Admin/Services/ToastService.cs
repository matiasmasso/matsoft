using System;

namespace Identity.Admin.Services;

public class ToastService
{
    public event Action<string, string>? OnShow; // (message, level)
    public event Action? OnHide;

    public void ShowSuccess(string message) => Show(message, "success");
    public void ShowError(string message) => Show(message, "error");
    public void ShowInfo(string message) => Show(message, "info");

    private void Show(string message, string level)
    {
        OnShow?.Invoke(message, level);
    }

    public void Hide() => OnHide?.Invoke();
}
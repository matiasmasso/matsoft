using System;

namespace MatComponents.Services;

public class ToastService
{
    public event Action<ToastLevel, string>? OnShow; // (message, level)
    public event Action? OnHide;


    public void ShowSuccess(string message)
    => OnShow?.Invoke(ToastLevel.Success, message);

    public void ShowError(string message)
        => OnShow?.Invoke(ToastLevel.Error, message);
    public void ShowInfo(string message)
        => OnShow?.Invoke(ToastLevel.Info, message);


    public void Hide() => OnHide?.Invoke();

    public void Show(ToastLevel level, string message)
    {
        OnShow?.Invoke(level, message);
    }
}



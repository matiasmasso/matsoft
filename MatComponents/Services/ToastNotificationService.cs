using ApiCore.Notifications;

namespace MatComponents.Services;

public class ToastNotificationService : INotificationService
{
    private readonly ToastService _toast;

    public ToastNotificationService(ToastService toast)
    {
        _toast = toast;
    }

    public void ShowInfo(string message)
        => _toast.Show(ToastLevel.Info, message);

    public void ShowWarning(string message)
        => _toast.Show(ToastLevel.Warning, message);

    public void ShowError(string message)
        => _toast.Show(ToastLevel.Error, message);
}
using Identity.Admin.Services.UI;

public class ToastNotificationService : INotificationService
{
    private readonly ToastService _toast;

    public ToastNotificationService(ToastService toast)
    {
        _toast = toast;
    }

    public void ShowError(string message) => _toast.Error(message);
    public void ShowWarning(string message) => _toast.Warning(message);
    public void ShowInfo(string message) => _toast.Info(message);
}
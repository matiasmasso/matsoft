using Identity.Client.Notifications;

public sealed class ToastErrorNotifier : IErrorNotifier
{
    private readonly ToastService _toast;

    public ToastErrorNotifier(ToastService toast)
    {
        _toast = toast;
    }

    public void NotifyError(string message)
        => _toast.ShowError(message);

    public void NotifySuccess(string message)
        => _toast.ShowSuccess(message);
}

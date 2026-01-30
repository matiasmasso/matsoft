namespace Identity.Client.Notifications;

public interface IErrorNotifier
{
    void NotifyError(string message);
    void NotifySuccess(string message);
}

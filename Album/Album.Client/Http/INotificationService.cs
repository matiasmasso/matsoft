namespace Album.Client.Http
{
    public interface INotificationService
    {
        void ShowInfo(string message);
        void ShowWarning(string message);
        void ShowError(string message);
    }
}

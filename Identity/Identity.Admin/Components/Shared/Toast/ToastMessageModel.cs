namespace Identity.Admin.Components.Shared.Toast
{
    public sealed class ToastMessageModel
    {
        public string Text { get; }
        public ToastType Type { get; }
        public Action<ToastMessageModel> Remove { get; }
        public bool Sticky => Type == ToastType.Error;

        public ToastMessageModel(string text, ToastType type, Action<ToastMessageModel> remove)
        {
            Text = text;
            Type = type;
            Remove = remove;
        }
    }

    public enum ToastType
    {
        Success,
        Warning,
        Error
    }
}

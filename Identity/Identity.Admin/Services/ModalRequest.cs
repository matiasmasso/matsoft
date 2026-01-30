namespace Identity.Admin.Services
{
    public sealed class ModalRequest
    {
        public string Title { get; init; } = default!;
        public Type Component { get; init; } = default!;
        public IReadOnlyDictionary<string, object> Parameters { get; init; } = new Dictionary<string, object>();

    }
}

namespace Identity.Api.Entities;

public sealed class AppSecret
{
    public Guid Id { get; set; }
    public Guid AppId { get; set; }

    public string Provider { get; set; } = default!; // "Google", "Apple", "Microsoft", etc.
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;

    public App App { get; set; } = default!;
}
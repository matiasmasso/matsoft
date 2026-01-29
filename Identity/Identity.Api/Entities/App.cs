using Identity.Api.Entities;

public sealed class App
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string? Description { get; set; }
    public bool Enabled { get; set; }

    public List<AppRole> Roles { get; set; } = new();
    public List<UserApp> UserApps { get; set; } = new();
    public List<AppSecret> Secrets { get; set; } = new();
}
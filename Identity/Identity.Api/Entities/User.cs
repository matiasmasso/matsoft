using Identity.Api.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool Enabled { get; set; }

    public List<UserApp> UserApps { get; set; } = new();
}
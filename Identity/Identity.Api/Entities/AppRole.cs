public sealed class AppRole
{
    public Guid Id { get; set; }
    public Guid AppId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public App App { get; set; } = default!;
    public List<UserAppRole> UserAppRoles { get; set; } = new();
}
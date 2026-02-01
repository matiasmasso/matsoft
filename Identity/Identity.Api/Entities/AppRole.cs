using Microsoft.AspNetCore.Identity;

public sealed class AppRole : IdentityRole<Guid>
{
    public Guid AppId { get; set; }
    public string? Description { get; set; }

    public App App { get; set; } = default!;
    public List<UserAppRole> UserAppRoles { get; set; } = new();
}
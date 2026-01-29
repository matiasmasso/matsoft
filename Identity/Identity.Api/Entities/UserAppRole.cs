public sealed class UserAppRole
{
    public Guid UserId { get; set; }
    public Guid AppRoleId { get; set; }

    public User User { get; set; } = default!;
    public AppRole AppRole { get; set; } = default!;
}
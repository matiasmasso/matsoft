namespace Identity.Api.Domain.Users;

public class UserAppEnrollment
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid AppId { get; set; }

    public User User { get; set; } = default!;
    public App App { get; set; } = default!;
    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
}
namespace Identity.Api.Domain.Users;

public class UserAppEnrollment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AppKey { get; set; } = string.Empty;

    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
}
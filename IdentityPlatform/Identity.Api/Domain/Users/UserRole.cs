namespace Identity.Api.Domain.Users;

public class UserRole
{
    public Guid Id { get; set; }
    public Guid UserAppEnrollmentId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
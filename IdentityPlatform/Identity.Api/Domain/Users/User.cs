namespace Identity.Api.Domain.Users;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<UserAppEnrollment> Enrollments { get; set; } = new List<UserAppEnrollment>();
}
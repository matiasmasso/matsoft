using Identity.Api.Domain.Apps;

namespace Identity.Api.Domain.Users;

public class UserRole
{
    public Guid Id { get; set; }
    public Guid EnrollmentId { get; set; }
    public Guid RoleId { get; set; }

    public UserAppEnrollment Enrollment { get; set; } = default!;
    public AppRole Role { get; set; } = default!;
}
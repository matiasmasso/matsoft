using Identity.Api.Domain.Apps;
using Identity.Api.Domain.Users;

public class App
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<UserAppEnrollment> Enrollments { get; set; } = new();
    public List<AppRole> Roles { get; set; } = new();
}
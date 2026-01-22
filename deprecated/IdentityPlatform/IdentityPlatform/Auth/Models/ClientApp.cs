namespace IdentityPlatform.Auth.Models;

public class ClientApp
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string ClientId { get; set; } = default!;

    public List<UserAppEnrollment> Enrollments { get; set; } = new();
}
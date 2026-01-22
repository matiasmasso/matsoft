using IdentityPlatform.Server.Domain.Enums;

namespace IdentityPlatform.Server.Domain.Models;

public class UserAppEnrollment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid ClientAppId { get; set; }

    public AppRole Role { get; set; }

    // Navigation
    public User? User { get; set; }
    public ClientApp? ClientApp { get; set; }
}
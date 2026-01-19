
namespace IdentityServer.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;
    public string NormalizedUserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NormalizedEmail { get; set; } = default!;
    public bool EmailConfirmed { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public ICollection<ExternalLogin> ExternalLogins { get; set; } = new List<ExternalLogin>();
}
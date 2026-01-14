namespace Identity.Domain.Entities;


public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    public ICollection<UserApp> Apps { get; set; } = new List<UserApp>();
}


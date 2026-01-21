namespace Identity.Domain.Entities
{
    public class Application
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserApplication> Users { get; set; } = new List<UserApplication>();
        public ICollection<ApplicationRole> Roles { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }

}

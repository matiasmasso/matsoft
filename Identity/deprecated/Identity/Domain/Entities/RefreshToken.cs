namespace Identity.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }
        public string ReplacedByToken { get; set; }

        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }

        public ApplicationUser User { get; set; }
        public Application Application { get; set; }
    }

}

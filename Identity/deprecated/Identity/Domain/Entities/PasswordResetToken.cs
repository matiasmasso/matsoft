namespace Identity.Domain.Entities
{
    public class PasswordResetToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }

        public DateTime? UsedAt { get; set; }
        public bool IsUsed { get; set; }

        public ApplicationUser User { get; set; }
    }

}

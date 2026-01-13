namespace Identity.Domain.Entities
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public Guid AppRoleId { get; set; }
        public AppRole AppRole { get; set; } = default!;
    }

}

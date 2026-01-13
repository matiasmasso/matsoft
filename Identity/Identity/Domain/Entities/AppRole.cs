namespace Identity.Domain.Entities
{
    public class AppRole
    {
        public Guid AppRoleId { get; set; }

        public Guid AppId { get; set; }
        public App App { get; set; } = default!;

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}

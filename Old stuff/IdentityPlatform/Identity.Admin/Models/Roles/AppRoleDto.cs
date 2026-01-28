namespace Identity.Admin.Models.Roles
{
    public class AppRoleDto
    {
        public Guid Id { get; set; }
        public Guid AppId { get; set; }
        public string Name { get; set; } = default!;
    }
}

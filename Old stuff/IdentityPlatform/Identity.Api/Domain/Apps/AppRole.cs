using Identity.Api.Domain.Users;

namespace Identity.Api.Domain.Apps;

public class AppRole
{
    public Guid Id { get; set; }
    public Guid AppId { get; set; }
    public string Name { get; set; } = string.Empty;  // e.g. "Admin", "Editor"

    public App App { get; set; } = default!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
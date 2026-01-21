namespace Identity.Api.Models;

public class App
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;      // e.g. "Identity.Admin"
    public string ClientId { get; set; } = string.Empty;  // logical client id
    public string? Description { get; set; }

    public ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}
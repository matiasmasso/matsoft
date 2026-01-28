namespace Identity.Api.Models;

public class AppUserRole
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = default!;

    public int AppId { get; set; }
    public App App { get; set; } = default!;

    public string RoleName { get; set; } = string.Empty; // e.g. "Admin", "User"
}
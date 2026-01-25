namespace Identity.Shared.Models;

public class UserEnrollmentDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}

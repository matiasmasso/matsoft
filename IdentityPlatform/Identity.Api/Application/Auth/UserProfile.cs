namespace Identity.Api.Application.Auth;

public class UserProfile
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;

    public List<AppProfile> Apps { get; set; } = new();
}


namespace Identity.Api.Models.Users;

public class UserProfileUpdateDto
{
    public string Name { get; set; } = default!;
    public Dictionary<string, string>? Preferences { get; set; }
}
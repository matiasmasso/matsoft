public class UserProfileDto
{
    public string Email { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? AvatarUrl { get; set; }
    public Dictionary<string, string>? Preferences { get; set; }
}
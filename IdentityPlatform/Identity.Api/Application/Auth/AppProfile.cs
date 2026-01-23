namespace Identity.Api.Application.Auth;

public class AppProfile
{
    public string Key { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}
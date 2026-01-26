namespace Album.Client.Utils;

public static class ClaimPrettifier
{
    private static readonly Dictionary<string, string> Map = new()
    {
        ["sub"] = "User ID",
        ["email"] = "Email",
        ["name"] = "Name",
        ["preferred_username"] = "Username",
        ["picture"] = "Avatar",
        ["role"] = "Roles",
        ["iss"] = "Issuer",
        ["aud"] = "Audience"
    };

    public static string Pretty(string claimType)
        => Map.TryGetValue(claimType, out var pretty) ? pretty : claimType;
}
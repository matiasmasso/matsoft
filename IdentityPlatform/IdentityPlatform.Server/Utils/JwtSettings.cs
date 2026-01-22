namespace IdentityPlatform.Server.Utils;

public class JwtSettings
{
    public string Key { get; set; } = "";
    public string Issuer { get; set; } = "";
    public int ExpirationMinutes { get; set; } = 60;
}
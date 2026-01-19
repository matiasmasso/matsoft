public class OdbcClient
{
    public Guid Id { get; set; }

    public string ClientId { get; set; } = default!;
    public string ClientName { get; set; } = default!;
    public string? ClientSecret { get; set; }

    public string RedirectUrisJson { get; set; } = "[]";
    public string PostLogoutRedirectUrisJson { get; set; } = "[]";
    public string AllowedCorsOriginsJson { get; set; } = "[]";
    public string AllowedScopesJson { get; set; } = "[]";

    public bool RequirePkce { get; set; } = true;
    public bool RequireClientSecret { get; set; } = false;

    public DateTime CreatedAt { get; set; }

}
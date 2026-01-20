namespace IdentityServer.Api.Models.OAuth;

public class TokenRequest
{
    public string GrantType { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string RedirectUri { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string CodeVerifier { get; set; } = default!;
}
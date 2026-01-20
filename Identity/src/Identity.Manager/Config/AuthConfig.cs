namespace Identity.Manager.Config;

public static class AuthConfig
{
    //public const string Authority = "https://localhost:7105";
    public const string Authority = "https://local.identityserver.test:7105";

    public const string ClientId = "identity-manager";
    public const string RedirectUri = "https://localhost:7273/auth/callback";
    public const string PostLogoutRedirectUri = "https://localhost:7273/";
    public const string Scope = "openid profile email roles";
}
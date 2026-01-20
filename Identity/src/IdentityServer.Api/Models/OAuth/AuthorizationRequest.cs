namespace IdentityServer.Api.Models.OAuth
{
    public class AuthorizationRequest
    {
        public string ClientId { get; set; } = default!;
        public string RedirectUri { get; set; } = default!;
        public string ResponseType { get; set; } = default!;
        public string Scope { get; set; } = default!;
        public string CodeChallenge { get; set; } = default!;
        public string CodeChallengeMethod { get; set; } = default!;
    }
}

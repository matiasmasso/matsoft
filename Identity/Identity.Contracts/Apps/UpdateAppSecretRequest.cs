namespace Identity.Contracts.Apps;

public sealed class UpdateAppSecretRequest
{
    public Guid Id { get; set; }
    public Guid AppId { get; set; }  
    public string Provider { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}
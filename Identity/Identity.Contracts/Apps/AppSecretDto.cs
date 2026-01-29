namespace Identity.Contracts.Apps;

public sealed class AppSecretDto
{
    public Guid Id { get; set; }
    public string Provider { get; set; } = default!;
    public string ClientId { get; set; } = default!;
}
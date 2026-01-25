public class CreateAppRequest
{
    public string Key { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string ClientSecret { get; set; } = default!;
}
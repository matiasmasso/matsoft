namespace Identity.Admin.Models.Apps;

public class UpdateAppRequest
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string ClientSecret { get; set; } = default!;
}
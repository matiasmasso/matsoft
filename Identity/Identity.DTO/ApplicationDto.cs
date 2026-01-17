namespace Identity.DTO;

public class ApplicationDto
{
    public Guid ApplicationId { get; set; }
    public string? Name { get; set; }
    public string? ClientId { get; set; }
    public bool IsActive { get; set; }
}


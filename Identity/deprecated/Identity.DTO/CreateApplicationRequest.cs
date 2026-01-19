namespace Identity.DTO;


public class CreateApplicationRequest
{
    public string Name { get; set; } = "";
    public string ClientId { get; set; } = "";
    public bool IsActive { get; set; } = true;
}



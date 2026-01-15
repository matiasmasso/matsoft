namespace Identity.DTO;


public class UserDetailsDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = "";
    public string UserName { get; set; } = "";
    public List<UserApplicationDto> Applications { get; set; } = new();
}


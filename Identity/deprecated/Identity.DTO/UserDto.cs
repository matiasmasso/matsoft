namespace Identity.DTO;


public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<ApplicationDto> Applications { get; set; } = new();
    public List<UserRoleDto> Roles { get; set; } = new();
}

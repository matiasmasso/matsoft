namespace Identity.Api.Models;

public class TestUser
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
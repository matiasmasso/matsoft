namespace Identity.Domain.Entities;

public class UserApp
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!; 
    public Guid AppId { get; set; }
    public App App { get; set; } = default!;
}

namespace Identity.Domain.Entities;

public class UserApplication
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public Guid ApplicationId { get; set; }
    public Application Application { get; set; }

    public bool IsActive { get; set; }
}

namespace Identity.Models.DTOs;

public class EnrollUserRequest
{
    public Guid UserId { get; set; }
    public Guid ApplicationId { get; set; }
}

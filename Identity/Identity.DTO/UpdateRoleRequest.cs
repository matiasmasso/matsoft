namespace Identity.DTO;

public class UpdateRoleRequest
{
    public string Name { get; set; }
    public Guid? ApplicationId { get; set; } // null = global
}

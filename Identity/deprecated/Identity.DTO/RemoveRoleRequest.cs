namespace Identity.DTO;

public class RemoveRoleRequest
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Guid ApplicationId { get; set; }
    }


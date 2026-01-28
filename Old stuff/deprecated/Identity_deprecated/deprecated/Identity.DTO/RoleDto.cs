namespace Identity.DTO;

public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ApplicationId { get; set; }
    }

namespace Identity.Models.DTOs
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public Guid? ApplicationId { get; set; } // null = global role
    }

}

namespace Identity.Models.DTOs
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid ApplicationId { get; set; }
    }

}

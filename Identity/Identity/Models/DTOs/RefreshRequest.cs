namespace Identity.Models.DTOs
{
    public class RefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public Guid ApplicationId { get; set; }
    }
}

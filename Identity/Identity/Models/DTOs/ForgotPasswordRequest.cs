namespace Identity.Models.DTOs
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = default!;
        public string ClientUrl { get; set; } = default!;
    }

}

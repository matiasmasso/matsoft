namespace Identity.DTO;

public class ResendVerificationRequest
    {
        public string Email { get; set; }
        public string VerificationUrl { get; set; }
    }

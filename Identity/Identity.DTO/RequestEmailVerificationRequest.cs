namespace Identity.DTO;

public class RequestEmailVerificationRequest
    {
        public string Email { get; set; }
        public string VerificationUrl { get; set; } // URL of your Blazor page
    }





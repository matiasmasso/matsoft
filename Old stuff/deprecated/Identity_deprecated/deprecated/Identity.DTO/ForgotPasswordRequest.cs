namespace Identity.DTO;

public class ForgotPasswordRequest
    {
        public string Email { get; set; }
        public string ResetUrl { get; set; } // URL of your Blazor page
    }


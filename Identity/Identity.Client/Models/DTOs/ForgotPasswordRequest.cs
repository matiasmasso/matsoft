using System.ComponentModel.DataAnnotations;

namespace Identity.Client.Models.DTOs
{
    public class ForgotPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // URL of the client app (WASM) to build the reset link
        [Required]
        public string ClientUrl { get; set; } = string.Empty;
    }


}

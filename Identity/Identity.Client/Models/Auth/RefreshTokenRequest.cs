using System.ComponentModel.DataAnnotations;

namespace Identity.Models.Auth
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }

}

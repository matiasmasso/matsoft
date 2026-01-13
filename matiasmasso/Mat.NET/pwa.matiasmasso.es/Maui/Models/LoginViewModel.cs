using System.ComponentModel.DataAnnotations;

namespace Maui.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


    }
}

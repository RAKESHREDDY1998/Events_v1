using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(100)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [StringLength(100)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Events_v1.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Display(Name = "Check if Admin")]
        public bool AdminStatus { get; set; }
    }
}

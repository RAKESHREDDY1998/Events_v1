using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.DomainModels
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
    }
}

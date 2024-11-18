using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.DomainModels
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}

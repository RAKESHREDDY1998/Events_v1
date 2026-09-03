using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.DomainModels
{
    public class Event
    {
        public int EventId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 10000.00, ErrorMessage = "Ticket price must be between 0.01 and 10,000.")]
        public decimal TicketPrice { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        public Category? Category { get; set; }
        [Required]
        public string CategoryId { get; set; } = string.Empty;
    }
}

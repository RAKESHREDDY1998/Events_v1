using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.DomainModels
{
    public class Event
    {
        public int EventId { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public double TicketPrice { get; set; }

        [Required]
        public string Description { get; set; }
        public Category? Category { get; set; }
        [Required]
        public string CategoryId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.DomainModels
{
    public class Sale
    {
        public int SaleId { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }

        [Required]
        public string SaleDate { get; set; }
        [Required]
        public int TicketCount { get; set; }
        [Required]
        public string Delivery { get; set; }
        [Required]
        public double SubTotal { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double DeliveryCharge { get; set; }
        [Required]
        public double AmountDue { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}

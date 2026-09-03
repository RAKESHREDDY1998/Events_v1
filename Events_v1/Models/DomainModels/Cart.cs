using Events_v1.Models.Data;

namespace Events_v1.Models.DomainModels
{
    public class Cart
    {
        private const decimal SeniorDiscountRate = 0.2m;
        private const decimal MailDeliveryCharge = 3.95m;

        public Event Event { get; set; } = null!;
        public Sale Sale { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        public string SelectedDelivery { get; set; } = string.Empty;
        public bool SeniorDiscount { get; set; }
        public int Count { get; set; }

        public void ProcessSale(EventContext context)
        {
            //Calculates sale receipt values and sets the sale date
            Sale = new Sale
            {
                SaleDate = DateTime.Now,
                TicketCount = Count,
                Customer = Customer,
                Event = Event
            };
            Sale.SubTotal = Event.TicketPrice * Sale.TicketCount;
            if (SeniorDiscount)
            {
                Sale.Discount = Math.Round(Sale.SubTotal * SeniorDiscountRate, 2, MidpointRounding.AwayFromZero);
            }
            switch (SelectedDelivery)
            {
                case "M":
                    Sale.DeliveryCharge = MailDeliveryCharge;
                    Sale.Delivery = "Mail";
                    break;
                case "P":
                    Sale.Delivery = "Print at home";
                    break;
                case "D":
                    Sale.Delivery = "Digital ticket";
                    break;
                case "C":
                    Sale.Delivery = "Will call";
                    break;
            }
            Sale.AmountDue = Sale.SubTotal - Sale.Discount + Sale.DeliveryCharge;

            //Save data: the customer and the sale are written in one SaveChanges call so
            //they commit in a single transaction and a failure cannot leave an orphaned customer.
            context.Customers.Add(Customer);
            context.Sales.Add(Sale);
            context.SaveChanges();
        }
    }
}

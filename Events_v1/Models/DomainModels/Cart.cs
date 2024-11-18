using Events_v1.Models.Data;

namespace Events_v1.Models.DomainModels
{
    public class Cart
    {
        public Event Event { get; set; }
        public Sale Sale { get; set; }
        public Customer Customer { get; set; }
        public string SelectedDelivery { get; set; }
        public bool SeniorDiscount { get; set; }
        public int Count { get; set; }
        private void CalculateDiscount()
        {
            const double SENIOR_DISCOUNT = 0.2;
            Sale.Discount = Sale.SubTotal * SENIOR_DISCOUNT;
        }
        public void ProcessSale(EventContext context)
        {
            context.Customers.Add(Customer);
            context.SaveChanges();
            Sale = new Sale();
            //Calculates sale receipt values and sets the sale date
            Sale.SaleDate = DateTime.Today.ToShortDateString();
            Sale.TicketCount = Count;
            Sale.SubTotal = Event.TicketPrice * Sale.TicketCount;
            if (SeniorDiscount == true)
            {
                CalculateDiscount();
            }
            if (SelectedDelivery == "M")
            {
                Sale.DeliveryCharge = 3.95;
                Sale.Delivery = "Mail";
            }
            else if (SelectedDelivery == "P")
                Sale.Delivery = "Print at home";
            else if (SelectedDelivery == "D")
                Sale.Delivery = "Digital ticket";
            else if (SelectedDelivery == "C")
                Sale.Delivery = "Will call";
            Sale.AmountDue = Sale.SubTotal - Sale.Discount + Sale.DeliveryCharge;
            //Save data
            Sale.CustomerId = Customer.CustomerId;
            Sale.EventId = Event.EventId;
            Sale.Customer = Customer;
            Sale.Event = Event;
            context.Sales.Add(Sale);
            context.SaveChanges();
        }
    }
}

using Events_v1.Models.DomainModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Events_v1.Models.ViewModels
{
    public class CartViewModel
    {
        public string EventTitle { get; set; }
        public double TicketPrice { get; set; }
        public int EventId { get; set; }
        public Customer Customer { get; set; }
        [Required(ErrorMessage = "Please select a delivery option!")]
        public string SelectedDelivery { get; set; }
        public bool SeniorDiscount { get; set; }
        [Required(ErrorMessage = "Please enter count of tickets!")]
        [DataType(DataType.Text)]
        public int Count { get; set; }
        public Dictionary<string, string> DeliveryOptions { get; set; } = new Dictionary<string, string>
        {
            { "M", "Mail" },
            { "P", "Print at home"},
            { "D", "Digital ticket"},
            { "C", "Will call"}
        };

        //List for the drop down list
        public List<SelectListItem> DeliveryDropDown = new List<SelectListItem>();
        public CartViewModel()
        {
            //Constructor
            SetUpDropDown();
            Customer = new Customer();
        }
        public void SetUpDropDown()
        {
            //creates the drop down list items using the collection of options from the object
            foreach (KeyValuePair<string, string> kv in DeliveryOptions)
            {
                DeliveryDropDown.Add(new SelectListItem
                {
                    Value = kv.Key,
                    Text = kv.Value
                });
            }
        }
    }
}

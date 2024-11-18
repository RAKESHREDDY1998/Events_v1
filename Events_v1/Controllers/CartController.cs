using Events_v1.Models.Data;
using Events_v1.Models.DomainModels;
using Events_v1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_v1.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private EventContext _context { get; set; }
        public CartController(EventContext ctx)
        {
            _context = ctx;
        }

        [Authorize]
        public IActionResult Buy(int id)
        {
            //gets the ID of event user wants to buy ticket for
            //and starts the checkout process
            CartViewModel viewData = new CartViewModel();
            Event eventToBuy = _context.Events.Find(id);
            viewData.EventId = eventToBuy.EventId;
            viewData.EventTitle = eventToBuy.Title;
            viewData.TicketPrice = eventToBuy.TicketPrice;
            return View(viewData);
        }
        public IActionResult Confirmation(CartViewModel model)
        {
            //model contains sale info user supplied in the form
            if (ModelState.IsValid)
            {
                Cart cart = new Cart();
                cart.Customer = model.Customer;
                cart.Event = _context.Events.Find(model.EventId);
                cart.SelectedDelivery = model.SelectedDelivery;
                cart.SeniorDiscount = model.SeniorDiscount;
                cart.Count = model.Count;
                //calls the Cart object method to calculate sale
                //and save data
                cart.ProcessSale(_context);
                //Cart object is passed to Confirmation view to display sale confirmation
                return View(cart);
            }
            else
            {
                model.SetUpDropDown();
                return View("Buy", model);
            }
        }
    }
}

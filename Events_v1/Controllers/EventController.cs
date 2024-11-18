using Events_v1.Models.Data;
using Events_v1.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_v1.Controllers
{
    public class EventController : Controller
    {
        private EventContext _context { get; set; }
        public EventController(EventContext ctx)
        {
            _context = ctx;
        }
        public IActionResult List()
        {
            List<Event> events = _context.Events.Include(c => c.Category).ToList();
            return View(events);
        }
    }
}

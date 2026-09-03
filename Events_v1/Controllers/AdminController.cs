using Events_v1.Models.Data;
using Events_v1.Models.DomainModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events_v1.Controllers
{
    [Authorize(Policy = "UserIsAdmin")]
    public class AdminController : Controller
    {
        private EventContext _context { get; set; }

        public AdminController(EventContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Event newEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(newEvent);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(newEvent);
        }

        public IActionResult List()
        {
            List<Event> events = _context.Events.Include(c => c.Category).OrderBy(ev => ev.Title).ToList();
            return View(events);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Event? eventToEdit = _context.Events.Find(id);
            if (eventToEdit is null)
            {
                return NotFound();
            }
            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.ToList();
            return View(eventToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Event eventToUpdate)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Update(eventToUpdate);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            // Redisplay with the same data the GET action supplies; without the
            // categories the view's drop-down loop throws a NullReferenceException.
            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.ToList();
            return View(eventToUpdate);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Event? eventToDelete = _context.Events.Find(id);
            if (eventToDelete is null)
            {
                return NotFound();
            }
            ViewBag.SaleCount = _context.Sales.Count(s => s.EventId == id);
            return View(eventToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int eventId)
        {
            Event? eventToDelete = _context.Events.Find(eventId);
            if (eventToDelete is null)
            {
                return NotFound();
            }
            int saleCount = _context.Sales.Count(s => s.EventId == eventId);
            if (saleCount > 0)
            {
                // Sales are the theatre's financial records; deleting an event must never remove them.
                ModelState.AddModelError("", $"This event has {saleCount} recorded sale(s) and cannot be deleted.");
                ViewBag.SaleCount = saleCount;
                return View(eventToDelete);
            }
            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult EventSales(int id)
        {
            List<Sale> sales = _context.Sales
            .Where(s => s.EventId == id)
            .Include(v => v.Event)
            .Include(c => c.Customer)
            .ToList();
            return View("Sales", sales);
        }

    }
}

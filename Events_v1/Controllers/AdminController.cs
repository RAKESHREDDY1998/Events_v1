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
            else
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View(newEvent);
            }
        }

        public IActionResult List()
        {
            List<Event> events = _context.Events.Include(c => c.Category).OrderBy(ev => ev.Title).ToList();
            return View(events);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            Event eventToEdit = _context.Events.Find(id);
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
            else
            {
                return View(eventToUpdate);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Event eventToDelete = _context.Events.Find(id);
            return View(eventToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Event eventToDelete)
        {
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

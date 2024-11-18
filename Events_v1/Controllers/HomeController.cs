using Events_v1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Events_v1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
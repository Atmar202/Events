using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

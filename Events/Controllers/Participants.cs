using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    public class Participants : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }
    }
}

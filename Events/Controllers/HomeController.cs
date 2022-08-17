using Events.Data;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Events.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EventsContext _context;

        public HomeController(EventsContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
                return _context.AddEvents != null ?
                              View(await _context.AddEvents.ToListAsync()) :
                              Problem("Entity set 'EventsContext.AddEvents'  is null.");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddEvents == null)
            {
                return Problem("Entity set 'EventsContext.AddEvents'  is null.");
            }
            var addEvents = await _context.AddEvents.FindAsync(id);
            if (addEvents != null)
            {
                _context.AddEvents.Remove(addEvents);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
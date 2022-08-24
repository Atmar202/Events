using Events.Data;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    public class AddEventsController : Controller
    {
        private readonly EventsContext _context;

        public AddEventsController(EventsContext context)
        {
            _context = context;
        }

        // GET: AddEvents
        public IActionResult Index()
        {
            // Create Page
            return View();
        }

        // POST: AddEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nimi,Toimumisaeg,Koht,Lisainfo")] AddEvents addEvents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addEvents);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return LocalRedirect("~/");
            }
            return View(addEvents);
        }
    }
}
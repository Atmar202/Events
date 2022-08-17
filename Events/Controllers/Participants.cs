using Events.Data;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.Controllers
{
    public class Participants : Controller
    {
        private readonly EventsContext _context;

        public Participants(EventsContext context)
        {
            _context = context;
        }

        /*
        public IActionResult Index()
        {
            return View();
        }*/

        public IActionResult Info()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddEvents == null)
            {
                return NotFound();
            }

            var addEvents = await _context.AddEvents
                .FirstOrDefaultAsync(m => m.Id == id);

            /*
            var privateParticipants = await _context.PrivateParticipants.FirstOrDefaultAsync(m => m.Id == id);

            var companyParticipants = await _context.CompanyParticipants.FirstOrDefaultAsync(m => m.Id == id);
            */

            if (addEvents == null) // || privateParticipants == null || companyParticipants
            {
                return NotFound();
            }

            /*
            Tuple<AddEvents,CompanyParticipants, PrivateParticipants> tupleView = 
                new Tuple<AddEvents, CompanyParticipants, PrivateParticipants>(addEvents, companyParticipants, privateParticipants);
            */

            return View(addEvents);
        }

    }
}

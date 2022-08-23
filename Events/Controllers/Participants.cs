using Events.Data;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Events.Controllers
{
    public class Participants : Controller
    {
        private readonly EventsContext _context;

        public Participants(EventsContext context)
        {
            _context = context;
        }

        [Route("{controller}/{eventsId}/{action}/{id}/")]
        public async Task<IActionResult> PrivateParticipant(int? id, int? eventsId)
        {
            if (id == null || _context.PrivateParticipants == null)
            {
                return NotFound();
            }

            var addEvents = await _context.AddEvents.FirstOrDefaultAsync(m => m.Id == eventsId);

            var privateParticipants = await _context.PrivateParticipants.FindAsync(id);

            if (privateParticipants == null)
            {
                return NotFound();
            }

            EventsDetailsViewModel viewModel = await GetEventsDetailsViewModel(addEvents);

            viewModel.privateParticipantsModel = privateParticipants;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/{eventsId}/{action}/{id}/")]
        public async Task<IActionResult> PrivateParticipant([Bind("PrivateId, Eesnimi, Perekonnanimi, Isikukood, Maksmisviis, Lisainfo, EventsId")] int id, int eventsId, EventsDetailsViewModel viewModel)
        {
            var addEvents = await _context.AddEvents.FirstOrDefaultAsync(m => m.Id == eventsId);

            var privateParticipants = await _context.PrivateParticipants.FirstOrDefaultAsync(x => x.PrivateId == id);

            if (privateParticipants == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                privateParticipants.Eesnimi = viewModel.privateParticipantsModel.Eesnimi;
                privateParticipants.Perekonnanimi = viewModel.privateParticipantsModel.Perekonnanimi;
                privateParticipants.Isikukood = viewModel.privateParticipantsModel.Isikukood;
                privateParticipants.Maksmisviis = viewModel.privateParticipantsModel.Maksmisviis;
                privateParticipants.Lisainfo = viewModel.privateParticipantsModel.Lisainfo;

                _context.Update(privateParticipants);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = eventsId });
            }

            viewModel = await GetEventsDetailsViewModel(addEvents);

            return View("PrivateParticipant", viewModel);
        }

        [Route("{controller}/{eventsId}/{action}/{id}/")]
        public async Task<IActionResult> CompanyParticipant(int? id, int? eventsId)
        {

            if (id == null || _context.CompanyParticipants == null)
            {
                return NotFound();
            }

            var addEvents = await _context.AddEvents.FirstOrDefaultAsync(m => m.Id == eventsId);

            var companyParticipants = await _context.CompanyParticipants.FindAsync(id);

            if (companyParticipants == null)
            {
                return NotFound();
            }

            EventsDetailsViewModel viewModel = await GetEventsDetailsViewModel(addEvents);

            viewModel.companyParticipantsModel = companyParticipants;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{controller}/{eventsId}/{action}/{id}/")]
        public async Task<IActionResult> CompanyParticipant([Bind("CompanyId, Nimi, Registrikood, Osavõtjate_arv, Maksmiseviis, Lisainfo, EventsId")] int id, int eventsId, EventsDetailsViewModel viewModel)
        {
            var addEvents = await _context.AddEvents.FirstOrDefaultAsync(m => m.Id == eventsId);

            var companyParticipants = await _context.CompanyParticipants.FirstOrDefaultAsync(x => x.CompanyId == id);

            if (companyParticipants == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                companyParticipants.Nimi = viewModel.companyParticipantsModel.Nimi;
                companyParticipants.Registrikood = viewModel.companyParticipantsModel.Registrikood;
                companyParticipants.Osavõtjate_arv = viewModel.companyParticipantsModel.Osavõtjate_arv;
                companyParticipants.Maksmiseviis = viewModel.companyParticipantsModel.Maksmiseviis;
                companyParticipants.Lisainfo = viewModel.companyParticipantsModel.Lisainfo;

                _context.Update(companyParticipants);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = eventsId });
            }

            viewModel = await GetEventsDetailsViewModel(addEvents);

            return View("PrivateParticipant", viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddEvents == null)
            {
                return NotFound();
            }

            var addEvents = await _context.AddEvents.FirstOrDefaultAsync(m => m.Id == id);

            if (addEvents == null)
            {
                return NotFound();
            }

            EventsDetailsViewModel viewModel = await GetEventsDetailsViewModel(addEvents);

            return View(viewModel);
        }

        private async Task<EventsDetailsViewModel> GetEventsDetailsViewModel(AddEvents addEvents)
        {
            EventsDetailsViewModel viewModel = new EventsDetailsViewModel();

            viewModel.Events = addEvents;

            List<PrivateParticipants> private_participants = await _context.PrivateParticipants.Where(m => m.Events == addEvents).ToListAsync();

            viewModel.privateParticipants = private_participants;

            List<CompanyParticipants> company_participants = await _context.CompanyParticipants.Where(m => m.Events == addEvents).ToListAsync();

            viewModel.companyParticipants = company_participants;

            return viewModel;
        }

        public async Task<IActionResult> CreateCompanyParticipant([Bind("Nimi, Registrikood, Osavõtjate_arv, Maksmiseviis, Lisainfo, EventsId")] IFormCollection values, EventsDetailsViewModel viewModel)
        {
            var addEvents = await _context.AddEvents
                .FirstOrDefaultAsync(m => m.Id == viewModel.EventsId);

            if (addEvents == null || _context.CompanyParticipants == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                CompanyParticipants companyParticipants = new CompanyParticipants();

                companyParticipants.Nimi = viewModel.companyParticipantsModel.Nimi;
                companyParticipants.Registrikood = viewModel.companyParticipantsModel.Registrikood;
                companyParticipants.Osavõtjate_arv = viewModel.companyParticipantsModel.Osavõtjate_arv;
                companyParticipants.Maksmiseviis = viewModel.companyParticipantsModel.Maksmiseviis;
                companyParticipants.Lisainfo = viewModel.companyParticipantsModel.Lisainfo;

                companyParticipants.Events = addEvents;
                _context.CompanyParticipants.Add(companyParticipants);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            viewModel = await GetEventsDetailsViewModel(addEvents);

            return View("Details", viewModel);
        }

        public async Task<IActionResult> CreatePrivateParticipant([Bind("Eesnimi, Perekonnanimi, Isikukood, Maksmisviis, Lisainfo, EventsId")] IFormCollection values, EventsDetailsViewModel viewModel)
        {
            var addEvents = await _context.AddEvents
                .FirstOrDefaultAsync(m => m.Id == viewModel.EventsId);

            if (addEvents == null || _context.PrivateParticipants == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                PrivateParticipants privateParticipants = new PrivateParticipants();

                privateParticipants.Eesnimi = viewModel.privateParticipantsModel.Eesnimi;
                privateParticipants.Perekonnanimi = viewModel.privateParticipantsModel.Perekonnanimi;
                privateParticipants.Isikukood = viewModel.privateParticipantsModel.Isikukood;
                privateParticipants.Maksmisviis = viewModel.privateParticipantsModel.Maksmisviis;
                privateParticipants.Lisainfo = viewModel.privateParticipantsModel.Lisainfo;

                privateParticipants.Events = addEvents;
                _context.PrivateParticipants.Add(privateParticipants);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            viewModel = await GetEventsDetailsViewModel(addEvents);

            return View("Details", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePrivateParticipant(int id, int eventsId)
        {
            if (_context.PrivateParticipants == null)
            {
                return Problem("Entity set 'EventsContext.PrivateParticipants'  is null.");
            }
            var privateParticipants = await _context.PrivateParticipants.FindAsync(id);
            if (privateParticipants != null)
            {
                _context.PrivateParticipants.Remove(privateParticipants);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = eventsId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompanyParticipant(int id, int eventsId)
        {
            if (_context.CompanyParticipants == null)
            {
                return Problem("Entity set 'EventsContext.CompanyParticipants'  is null.");
            }
            var companyParticipants = await _context.CompanyParticipants.FindAsync(id);
            if (companyParticipants != null)
            {
                _context.CompanyParticipants.Remove(companyParticipants);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = eventsId });
        }
    }
}

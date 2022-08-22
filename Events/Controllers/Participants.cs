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

            IEnumerable<PrivateParticipants> private_participants = await _context.PrivateParticipants.Where(m => m.Events == addEvents).ToListAsync();

            viewModel.privateParticipants = private_participants;

            IEnumerable<CompanyParticipants> company_participants = await _context.CompanyParticipants.Where(m => m.Events == addEvents).ToListAsync();

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

    }
}

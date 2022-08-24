using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Events.Data;
using Events.Models;

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

        // GET: AddEvents/Details/5
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

        // GET: Admin
        public async Task<IActionResult> Admin()
        {
            return _context.AddEvents != null ?
                          View(await _context.AddEvents.ToListAsync()) :
                          Problem("Entity set 'EventsContext.AddEvents'  is null.");
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

        // GET: AddEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddEvents == null)
            {
                return NotFound();
            }

            var addEvents = await _context.AddEvents.FindAsync(id);
            if (addEvents == null)
            {
                return NotFound();
            }
            return View(addEvents);
        }

        // POST: AddEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nimi,Toimumisaeg,Koht,Lisainfo")] AddEvents addEvents)
        {
            if (id != addEvents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddEventsExists(addEvents.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Admin));
            }
            return View(addEvents);
        }

        // GET: AddEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddEvents == null)
            {
                return NotFound();
            }

            var addEvents = await _context.AddEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addEvents == null)
            {
                return NotFound();
            }

            return View(addEvents);
        }

        // POST: AddEvents/Delete/5
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
            return RedirectToAction(nameof(Admin));
        }

        private bool AddEventsExists(int id)
        {
          return (_context.AddEvents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

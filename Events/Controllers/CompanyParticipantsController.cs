using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Data;
using Events.Models;

namespace Events.Controllers
{
    public class CompanyParticipantsController : Controller
    {
        private readonly EventsContext _context;

        public CompanyParticipantsController(EventsContext context)
        {
            _context = context;
        }

        // GET: CompanyParticipants
        public async Task<IActionResult> Index()
        {
              return _context.CompanyParticipants != null ? 
                          View(await _context.CompanyParticipants.ToListAsync()) :
                          Problem("Entity set 'EventsContext.CompanyParticipants'  is null.");
        }

        // GET: CompanyParticipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompanyParticipants == null)
            {
                return NotFound();
            }

            var companyParticipants = await _context.CompanyParticipants
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (companyParticipants == null)
            {
                return NotFound();
            }

            return View(companyParticipants);
        }

        // GET: CompanyParticipants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyParticipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,Nimi,Registrikood,Osavõtjate_arv,Maksmiseviis,Lisainfo")] CompanyParticipants companyParticipants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyParticipants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyParticipants);
        }

        // GET: CompanyParticipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompanyParticipants == null)
            {
                return NotFound();
            }

            var companyParticipants = await _context.CompanyParticipants.FindAsync(id);
            if (companyParticipants == null)
            {
                return NotFound();
            }
            return View(companyParticipants);
        }

        // POST: CompanyParticipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,Nimi,Registrikood,Osavõtjate_arv,Maksmiseviis,Lisainfo")] CompanyParticipants companyParticipants)
        {
            if (id != companyParticipants.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyParticipants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyParticipantsExists(companyParticipants.CompanyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(companyParticipants);
        }

        // GET: CompanyParticipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompanyParticipants == null)
            {
                return NotFound();
            }

            var companyParticipants = await _context.CompanyParticipants
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (companyParticipants == null)
            {
                return NotFound();
            }

            return View(companyParticipants);
        }

        // POST: CompanyParticipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyParticipantsExists(int id)
        {
          return (_context.CompanyParticipants?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }
    }
}

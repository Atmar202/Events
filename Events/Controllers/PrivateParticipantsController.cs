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
    public class PrivateParticipantsController : Controller
    {
        private readonly EventsContext _context;

        public PrivateParticipantsController(EventsContext context)
        {
            _context = context;
        }

        // GET: PrivateParticipants
        public async Task<IActionResult> Index()
        {
              return _context.PrivateParticipants != null ? 
                          View(await _context.PrivateParticipants.ToListAsync()) :
                          Problem("Entity set 'EventsContext.PrivateParticipants'  is null.");
        }

        // GET: PrivateParticipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PrivateParticipants == null)
            {
                return NotFound();
            }

            var privateParticipants = await _context.PrivateParticipants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (privateParticipants == null)
            {
                return NotFound();
            }

            return View(privateParticipants);
        }

        // GET: PrivateParticipants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrivateParticipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Eesnimi,Perekonnanimi,Isikukood,Maksmisviis,Lisainfo")] PrivateParticipants privateParticipants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(privateParticipants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(privateParticipants);
        }

        // GET: PrivateParticipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PrivateParticipants == null)
            {
                return NotFound();
            }

            var privateParticipants = await _context.PrivateParticipants.FindAsync(id);
            if (privateParticipants == null)
            {
                return NotFound();
            }
            return View(privateParticipants);
        }

        // POST: PrivateParticipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Eesnimi,Perekonnanimi,Isikukood,Maksmisviis,Lisainfo")] PrivateParticipants privateParticipants)
        {
            if (id != privateParticipants.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(privateParticipants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrivateParticipantsExists(privateParticipants.Id))
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
            return View(privateParticipants);
        }

        // GET: PrivateParticipants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PrivateParticipants == null)
            {
                return NotFound();
            }

            var privateParticipants = await _context.PrivateParticipants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (privateParticipants == null)
            {
                return NotFound();
            }

            return View(privateParticipants);
        }

        // POST: PrivateParticipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
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
            return RedirectToAction(nameof(Index));
        }

        private bool PrivateParticipantsExists(int id)
        {
          return (_context.PrivateParticipants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

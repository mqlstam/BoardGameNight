using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoardGameNight.Data;

namespace BoardGameNight.Models
{
    public class BordspellenavondController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BordspellenavondController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bordspellenavond
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bordspellenavonden.Include(b => b.Organisator);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bordspellenavond/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bordspellenavonden == null)
            {
                return NotFound();
            }

            var bordspellenavond = await _context.Bordspellenavonden
                .Include(b => b.Organisator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bordspellenavond == null)
            {
                return NotFound();
            }

            return View(bordspellenavond);
        }

        // GET: Bordspellenavond/Create
        public IActionResult Create()
        {
            ViewData["OrganisatorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Bordspellenavond/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adres,MaxAantalSpelers,DatumTijd,Is18Plus,OrganisatorId")] Bordspellenavond bordspellenavond)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bordspellenavond);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganisatorId"] = new SelectList(_context.Users, "Id", "Id", bordspellenavond.OrganisatorId);
            return View(bordspellenavond);
        }

        // GET: Bordspellenavond/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bordspellenavonden == null)
            {
                return NotFound();
            }

            var bordspellenavond = await _context.Bordspellenavonden.FindAsync(id);
            if (bordspellenavond == null)
            {
                return NotFound();
            }
            ViewData["OrganisatorId"] = new SelectList(_context.Users, "Id", "Id", bordspellenavond.OrganisatorId);
            return View(bordspellenavond);
        }

        // POST: Bordspellenavond/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adres,MaxAantalSpelers,DatumTijd,Is18Plus,OrganisatorId")] Bordspellenavond bordspellenavond)
        {
            if (id != bordspellenavond.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bordspellenavond);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BordspellenavondExists(bordspellenavond.Id))
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
            ViewData["OrganisatorId"] = new SelectList(_context.Users, "Id", "Id", bordspellenavond.OrganisatorId);
            return View(bordspellenavond);
        }

        // GET: Bordspellenavond/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bordspellenavonden == null)
            {
                return NotFound();
            }

            var bordspellenavond = await _context.Bordspellenavonden
                .Include(b => b.Organisator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bordspellenavond == null)
            {
                return NotFound();
            }

            return View(bordspellenavond);
        }

        // POST: Bordspellenavond/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bordspellenavonden == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bordspellenavonden'  is null.");
            }
            var bordspellenavond = await _context.Bordspellenavonden.FindAsync(id);
            if (bordspellenavond != null)
            {
                _context.Bordspellenavonden.Remove(bordspellenavond);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BordspellenavondExists(int id)
        {
          return (_context.Bordspellenavonden?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

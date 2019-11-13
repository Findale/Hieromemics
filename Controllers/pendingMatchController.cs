using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hieromemics.Data;
using Hieromemics.Models;

namespace Hieromemics.Controllers
{
    public class pendingMatchController : Controller
    {
        private readonly HieromemicsContext _context;

        public pendingMatchController(HieromemicsContext context)
        {
            _context = context;
        }

        // GET: pendingMatch
        public async Task<IActionResult> Index()
        {
            return View(await _context.pendingMatch.ToListAsync());
        }

        // GET: pendingMatch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingMatch = await _context.pendingMatch
                .FirstOrDefaultAsync(m => m.pendingId == id);
            if (pendingMatch == null)
            {
                return NotFound();
            }

            return View(pendingMatch);
        }

        // GET: pendingMatch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pendingMatch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("pendingId,lookingId,seekingId")] pendingMatch pendingMatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pendingMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pendingMatch);
        }

        // GET: pendingMatch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingMatch = await _context.pendingMatch.FindAsync(id);
            if (pendingMatch == null)
            {
                return NotFound();
            }
            return View(pendingMatch);
        }

        // POST: pendingMatch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("pendingId,lookingId,seekingId")] pendingMatch pendingMatch)
        {
            if (id != pendingMatch.pendingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pendingMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!pendingMatchExists(pendingMatch.pendingId))
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
            return View(pendingMatch);
        }

        // GET: pendingMatch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pendingMatch = await _context.pendingMatch
                .FirstOrDefaultAsync(m => m.pendingId == id);
            if (pendingMatch == null)
            {
                return NotFound();
            }

            return View(pendingMatch);
        }

        // POST: pendingMatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pendingMatch = await _context.pendingMatch.FindAsync(id);
            _context.pendingMatch.Remove(pendingMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool pendingMatchExists(int id)
        {
            return _context.pendingMatch.Any(e => e.pendingId == id);
        }
    }
}

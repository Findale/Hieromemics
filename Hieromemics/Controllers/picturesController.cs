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
    public class picturesController : Controller
    {
        private readonly HieromemicsContext _context;

        public picturesController(HieromemicsContext context)
        {
            _context = context;
        }

        // GET: pictures
        public async Task<IActionResult> Index()
        {
            return View(await _context.pictures.ToListAsync());
        }

        // GET: pictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pictures = await _context.pictures
                .FirstOrDefaultAsync(m => m.PicID == id);
            if (pictures == null)
            {
                return NotFound();
            }

            return View(pictures);
        }

        // GET: pictures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PicID,StoragePath")] pictures pictures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pictures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pictures);
        }

        // GET: pictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pictures = await _context.pictures.FindAsync(id);
            if (pictures == null)
            {
                return NotFound();
            }
            return View(pictures);
        }

        // POST: pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PicID,StoragePath")] pictures pictures)
        {
            if (id != pictures.PicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pictures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!picturesExists(pictures.PicID))
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
            return View(pictures);
        }

        // GET: pictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pictures = await _context.pictures
                .FirstOrDefaultAsync(m => m.PicID == id);
            if (pictures == null)
            {
                return NotFound();
            }

            return View(pictures);
        }

        // POST: pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pictures = await _context.pictures.FindAsync(id);
            _context.pictures.Remove(pictures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool picturesExists(int id)
        {
            return _context.pictures.Any(e => e.PicID == id);
        }
    }
}

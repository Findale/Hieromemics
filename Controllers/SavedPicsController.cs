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
    public class SavedPicsController : Controller
    {
        private readonly HieromemicsContext _context;

        public SavedPicsController(HieromemicsContext context)
        {
            _context = context;
        }

        // GET: SavedPics
        public async Task<IActionResult> Index()
        {
            var hieromemicsContext = _context.SavedPics.Include(s => s.pictures).Include(s => s.users);
            return View(await hieromemicsContext.ToListAsync());
        }

        // GET: SavedPics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedPics = await _context.SavedPics
                .Include(s => s.pictures)
                .Include(s => s.users)
                .FirstOrDefaultAsync(m => m.SavedPicID == id);
            if (savedPics == null)
            {
                return NotFound();
            }

            return View(savedPics);
        }

        // GET: SavedPics/Create
        public IActionResult Create()
        {
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath");
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID");
            return View();
        }

        // POST: SavedPics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SavedPicID,UserID,PicID")] SavedPics savedPics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savedPics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", savedPics.PicID);
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", savedPics.UserID);
            return View(savedPics);
        }

        // GET: SavedPics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedPics = await _context.SavedPics.FindAsync(id);
            if (savedPics == null)
            {
                return NotFound();
            }
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", savedPics.PicID);
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", savedPics.UserID);
            return View(savedPics);
        }

        // POST: SavedPics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SavedPicID,UserID,PicID")] SavedPics savedPics)
        {
            if (id != savedPics.SavedPicID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedPics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedPicsExists(savedPics.SavedPicID))
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
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", savedPics.PicID);
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", savedPics.UserID);
            return View(savedPics);
        }

        // GET: SavedPics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedPics = await _context.SavedPics
                .Include(s => s.pictures)
                .Include(s => s.users)
                .FirstOrDefaultAsync(m => m.SavedPicID == id);
            if (savedPics == null)
            {
                return NotFound();
            }

            return View(savedPics);
        }

        // POST: SavedPics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedPics = await _context.SavedPics.FindAsync(id);
            _context.SavedPics.Remove(savedPics);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedPicsExists(int id)
        {
            return _context.SavedPics.Any(e => e.SavedPicID == id);
        }
    }
}

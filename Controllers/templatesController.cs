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
    public class templatesController : Controller
    {
        private readonly HieromemicsContext _context;

        public templatesController(HieromemicsContext context)
        {
            _context = context;
        }

        // GET: templates
        public async Task<IActionResult> Index()
        {
            var hieromemicsContext = _context.templates.Include(t => t.pictures);
            return View(await hieromemicsContext.ToListAsync());
        }

        // GET: templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var templates = await _context.templates
                .Include(t => t.pictures)
                .FirstOrDefaultAsync(m => m.TemplateID == id);
            if (templates == null)
            {
                return NotFound();
            }

            return View(templates);
        }

        // GET: templates/Create
        public IActionResult Create()
        {
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath");
            return View();
        }

        // POST: templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TemplateID,PicID")] templates templates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(templates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", templates.PicID);
            return View(templates);
        }

        // GET: templates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var templates = await _context.templates.FindAsync(id);
            if (templates == null)
            {
                return NotFound();
            }
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", templates.PicID);
            return View(templates);
        }

        // POST: templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TemplateID,PicID")] templates templates)
        {
            if (id != templates.TemplateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(templates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!templatesExists(templates.TemplateID))
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
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", templates.PicID);
            return View(templates);
        }

        // GET: templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var templates = await _context.templates
                .Include(t => t.pictures)
                .FirstOrDefaultAsync(m => m.TemplateID == id);
            if (templates == null)
            {
                return NotFound();
            }

            return View(templates);
        }

        // POST: templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var templates = await _context.templates.FindAsync(id);
            _context.templates.Remove(templates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool templatesExists(int id)
        {
            return _context.templates.Any(e => e.TemplateID == id);
        }
    }
}

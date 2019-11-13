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
    public class messagesController : Controller
    {
        private readonly HieromemicsContext _context;

        public messagesController(HieromemicsContext context)
        {
            _context = context;
        }

        // GET: messages
        public async Task<IActionResult> Index()
        {
            var hieromemicsContext = _context.messages.Include(m => m.pictures).Include(m => m.users);
            return View(await hieromemicsContext.ToListAsync());
        }

        // GET: messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.messages
                .Include(m => m.pictures)
                .Include(m => m.users)
                .FirstOrDefaultAsync(m => m.messageID == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: messages/Create
        public IActionResult Create()
        {
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath");
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID");
            return View();
        }

        // POST: messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("messageID,PicID,UserID,FriendCode,timestamp")] messages messages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", messages.PicID);
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", messages.UserID);
            return View(messages);
        }

        // GET: messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", messages.PicID);
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", messages.UserID);
            return View(messages);
        }

        // POST: messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("messageID,PicID,UserID,FriendCode,timestamp")] messages messages)
        {
            if (id != messages.messageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!messagesExists(messages.messageID))
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
            ViewData["PicID"] = new SelectList(_context.pictures, "PicID", "StoragePath", messages.PicID);
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", messages.UserID);
            return View(messages);
        }

        // GET: messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.messages
                .Include(m => m.pictures)
                .Include(m => m.users)
                .FirstOrDefaultAsync(m => m.messageID == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messages = await _context.messages.FindAsync(id);
            _context.messages.Remove(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool messagesExists(int id)
        {
            return _context.messages.Any(e => e.messageID == id);
        }
    }
}

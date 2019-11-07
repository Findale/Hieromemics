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
    public class friendListController : Controller
    {
        private readonly HieromemicsContext _context;

        public friendListController(HieromemicsContext context)
        {
            _context = context;
        }

        // GET: friendList
        public async Task<IActionResult> Index()
        {
            var hieromemicsContext = _context.friendList.Include(f => f.users);
            return View(await hieromemicsContext.ToListAsync());
        }

        // GET: friendList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendList = await _context.friendList
                .Include(f => f.users)
                .FirstOrDefaultAsync(m => m.FriendListID == id);
            if (friendList == null)
            {
                return NotFound();
            }

            return View(friendList);
        }

        // GET: friendList/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID");
            return View();
        }

        // POST: friendList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FriendListID,UserID,FriendCode")] friendList friendList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friendList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", friendList.UserID);
            return View(friendList);
        }

        // GET: friendList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendList = await _context.friendList.FindAsync(id);
            if (friendList == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", friendList.UserID);
            return View(friendList);
        }

        // POST: friendList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FriendListID,UserID,FriendCode")] friendList friendList)
        {
            if (id != friendList.FriendListID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friendList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!friendListExists(friendList.FriendListID))
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
            ViewData["UserID"] = new SelectList(_context.users, "UserID", "UserID", friendList.UserID);
            return View(friendList);
        }

        // GET: friendList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friendList = await _context.friendList
                .Include(f => f.users)
                .FirstOrDefaultAsync(m => m.FriendListID == id);
            if (friendList == null)
            {
                return NotFound();
            }

            return View(friendList);
        }

        // POST: friendList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friendList = await _context.friendList.FindAsync(id);
            _context.friendList.Remove(friendList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool friendListExists(int id)
        {
            return _context.friendList.Any(e => e.FriendListID == id);
        }
    }
}

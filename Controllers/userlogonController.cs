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
    public class userlogonController : Controller
    {
        private readonly HieromemicsContext _context;

        public userlogonController(HieromemicsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string username)
        {
            if (username == "")
                return View(nameof(HomeController.Index));
            int usrs;
            try
            {
                usrs = _context.users.Where(u => u.userName == username).Select(u => u.UserID).Single();
            }
            catch(Exception)
            {
                return View(nameof(HomeController.Index));
            }
            var personyfrens = _context.friendList.Where(pf => pf.UserID == usrs).Select(pf => pf.FriendCode).Single();
            var frens = _context.users.Where(u => u.FriendCode == personyfrens).Select(cu => cu);
            ViewData["UserID"] = usrs;
            ViewData["UserName"] = username;
            return View(await frens.ToListAsync());
        }
    }
}
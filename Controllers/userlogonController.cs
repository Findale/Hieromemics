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
            //Console.WriteLine(username);
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
            var friendCodes = _context.friendList
                                .Where(u => u.UserID == usrs)
                                .Select(fc => fc.FriendCode);
            List<users> friends = (from code in friendCodes
                            from user in _context.users
                            where code == user.FriendCode
                            select user).ToList();
            ViewData["UserID"] = usrs;
            ViewData["UserName"] = username;
            ViewData["Friends"] = friends;
            return View(friends);
        }
    }
}
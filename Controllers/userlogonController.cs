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
            int usrs; 
                /*if (Cookie Exists) {
                    set usrs here
                } else {
                    return RedirectToAction("Index", "Home");
                }*/
            try
            {
                usrs = await _context.users.Where(u => u.userName == username).Select(u => u.UserID).SingleAsync();
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            var friendCodes = _context.friendList
                                .Where(u => u.UserID == usrs)
                                .Select(fc => fc.FriendCode);
            List<users> friends = await (from code in friendCodes
                            from user in _context.users
                            where code == user.FriendCode
                            select user).ToListAsync();
            var mycode = await _context.users.Where(u => u.userName == username).Select(u => u.FriendCode).SingleAsync();
            ViewData["UserID"] = usrs;
            ViewData["UserName"] = username;
            ViewData["Code"] = mycode;
            ViewData["Friends"] = friends;
            return View(friends);
        }
        public static Dictionary<int, string> activeSessions = new Dictionary<int, string>();
        public async Task<IActionResult> Chat(int uid, int fid)
        {
            var user = await _context.users.Select(u => u).Where(u => u.UserID == uid).SingleAsync();
            var fren = await _context.users.Select(f => f).Where(f => f.UserID == fid).SingleAsync();
            string grpid;
            if (!activeSessions.TryGetValue(fren.UserID, out grpid))
            {
                grpid = Guid.NewGuid().ToString();
                activeSessions.Add(user.UserID, grpid);
            }
            else
            {
                activeSessions.Remove(fren.UserID);
            }
            ViewData["uid"] = user.UserID;
            ViewData["user"] = fren.userName;
            ViewData["name"] = user.userName;
            ViewData["grpid"] = grpid;
            return View();
        }
    }
}
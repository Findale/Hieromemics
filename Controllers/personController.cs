using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hieromemics.Data;
using Hieromemics.Models;


namespace Hieromemics.Controllers {

    public class personController : Controller {

        private readonly HieromemicsContext _context;

        public personController(HieromemicsContext context) {

            _context = context;
        }

        public async Task<IActionResult> Index() {

            return View(await _context.users.ToListAsync());
        }








    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GaMeet.Data;
using GaMeet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GaMeet.Controllers
{
    [Authorize(Roles = "Gamer")]
    public class GamersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GamersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: GamersController
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var gamer = _context.Gamers.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (gamer == null)
            {
                return View("Create");
            }
            else
            {
                return View("Index", gamer);
            }
        }
        // GET: GamersController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gamer = await _context.Gamers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamer == null)
            {
                return NotFound();
            }
            return View(gamer);
        }
        // GET: GamersController/Create
        public IActionResult Create()
        {
            Gamer gamer = new Gamer();
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View(gamer);
        }
        // POST: GamersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Address,State,ZipCode,IdentityUserId")] Gamer gamer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            gamer.IdentityUserId = userId;
            _context.Gamers.Add(gamer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: GamersController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gamer = await _context.Gamers.FindAsync(id);
            if (gamer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", gamer.IdentityUserId);
            return View(gamer);
        }
        // POST: GamersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Address,State,ZipCode")] Gamer gamer)
        {
            if (id != gamer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gamerInDB = _context.Gamers.Single(m => m.Id == gamer.Id);
                    gamerInDB.FirstName = gamer.FirstName;
                    gamerInDB.LastName = gamer.LastName;
                    gamerInDB.Address = gamer.Address;
                    gamerInDB.State = gamer.State;
                    gamerInDB.ZipCode = gamer.ZipCode;
                    
                    //_context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamerExists(gamer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = gamer.Id.ToString() });
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", gamer.IdentityUserId);
            return View(gamer.Id);
        }
        // GET: GamersController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gamer = await _context.Gamers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamer == null)
            {
                return NotFound();
            }
            return View(gamer);
        }

        // POST: GamersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamer = await _context.Gamers.FindAsync(id);
            _context.Gamers.Remove(gamer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool GamerExists(int id)
        {
            return _context.Gamers.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GaMeet.Data;
using GaMeet.Models;
using System.Collections;

namespace GaMeet.Controllers
{
    [Authorize(Roles = "EventCoordinator")]
    public class EventCoordinatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventCoordinatorsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EventCoordinatorsController
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var eventCoordinator = _context.EventCoordinators.Where(c => c.IdentityUserId == userId).FirstOrDefault();
            if (eventCoordinator == null)
            {
                return View("Create");
            }
            else
            {
                return View("Index", eventCoordinator);
            }
        }

        // GET: EventCoordinatorsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventCoordinator = await _context.EventCoordinators
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventCoordinator == null)
            {
                return NotFound();
            }
            return View(eventCoordinator);
        }

        // GET: EventCoordinatorsController/Create
        public IActionResult Create()
        {
            EventCoordinator eventCoordinator = new EventCoordinator();
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View(eventCoordinator);
        }

        // POST: EventCoordinatorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Company,Address,State,Zip,PhoneNumber")] EventCoordinator eventCoordinator)
        {
            if (ModelState.IsValid)
            {
                if (eventCoordinator.Id == 0)
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    eventCoordinator.IdentityUserId = userId;
                    _context.Add(eventCoordinator);
                }
                else
                {
                    var eventCoordinatorInDB = _context.EventCoordinators.Single(m => m.Id == eventCoordinator.Id);
                    eventCoordinatorInDB.FirstName = eventCoordinator.FirstName;
                    eventCoordinatorInDB.LastName = eventCoordinator.LastName;
                    eventCoordinatorInDB.Company = eventCoordinator.Company;
                    eventCoordinatorInDB.Address = eventCoordinator.Address;
                    eventCoordinatorInDB.State = eventCoordinator.State;
                    eventCoordinatorInDB.ZipCode = eventCoordinator.ZipCode;
                    eventCoordinatorInDB.PhoneNumber = eventCoordinator.PhoneNumber;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventCoordinator.IdentityUserId);
            return View(eventCoordinator);
        }

        // GET: EventCoordinatorsController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var eventCoordinator = await _context.EventCoordinators.FindAsync(id);
            if (eventCoordinator == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventCoordinator.IdentityUserId);
            return View(eventCoordinator);
        }

        // POST: EventCoordinatorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Company,Address,State,Zip,PhoneNumber")] EventCoordinator eventCoordinator)
        {
            if (id != eventCoordinator.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var eventCoordinatorInDB = _context.EventCoordinators.Single(m => m.Id == eventCoordinator.Id);
                    eventCoordinatorInDB.FirstName = eventCoordinator.FirstName;
                    eventCoordinatorInDB.LastName = eventCoordinator.LastName;
                    eventCoordinatorInDB.Company = eventCoordinator.Company;
                    eventCoordinatorInDB.Address = eventCoordinator.Address;
                    eventCoordinatorInDB.State = eventCoordinator.State;
                    eventCoordinatorInDB.ZipCode = eventCoordinator.ZipCode;
                    eventCoordinatorInDB.PhoneNumber = eventCoordinator.PhoneNumber;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCoordinatorExists(eventCoordinator.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = eventCoordinator.Id.ToString() });
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", eventCoordinator.IdentityUserId);
            return View(eventCoordinator.Id);
        }
        // GET: EventCoordinatorsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventCoordinator = await _context.EventCoordinators
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventCoordinator == null)
            {
                return NotFound();
            }

            return View(eventCoordinator);
        }
        // POST: EventCoordinatorsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventCoordinator = await _context.EventCoordinators.FindAsync(id);
            _context.EventCoordinators.Remove(eventCoordinator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EventCoordinatorExists(int id)
        {
            return _context.EventCoordinators.Any(e => e.Id == id);
        }
    }
}

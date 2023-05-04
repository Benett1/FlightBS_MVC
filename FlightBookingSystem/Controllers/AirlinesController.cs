using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem;
using FlightBookingSystem.Models;

namespace FlightBookingSystem.Controllers
{
    public class AirlinesController : Controller
    {
        private readonly DBContext _context;

        public AirlinesController(DBContext context)
        {
            _context = context;
        }

        // GET: Airlines
        public async Task<IActionResult> Index()
        {
            if (GlobalState.UserRole == "Admin")
            {
                var llocationData = _context.Airlines.Include(a => a.LocationModel);
                return View(await llocationData.ToListAsync());
            }
            return NotFound();
        }

        // GET: Airlines/Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Airlines == null)
            {
                return NotFound();
            }

            var airlineModel = await _context.Airlines
                .Include(a => a.LocationModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airlineModel == null)
            {
                return NotFound();
            }
            return View(airlineModel);
        }  

        // GET: Airlines/Create
        public IActionResult Create()
        {
            ViewData["CountryOfOrigin"] = new SelectList(_context.Locations, "Country", "Country");
            ViewBag.Locations = _context.Locations.ToList();
            return View();
        }

        // POST: Airlines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, String countryOfOrigin, Guid id)
        {
            var country = _context.Locations.FirstOrDefault(c => c.Country == countryOfOrigin);
            AirlineModel airlineModel = new AirlineModel();
            if (ModelState.IsValid)
            {
                airlineModel.Id = Guid.NewGuid();
                airlineModel.Name = name;
                airlineModel.CountryOfOrigin = country.Id;
                _context.Add(airlineModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryOfOrigin"] = new SelectList(_context.Locations, "Id", "Id", airlineModel.CountryOfOrigin);
            return View(airlineModel);
        }

        // GET: Airlines/Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Airlines == null)
            {
                return NotFound();
            }

            var airlineModel = await _context.Airlines.FindAsync(id);
            if (airlineModel == null)
            {
                return NotFound();
            }
            ViewData["CountryOfOrigin"] = new SelectList(_context.Locations, "Country", "Country", airlineModel.CountryOfOrigin);
            return View(airlineModel);
        }

        // POST: Airlines/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,CountryOfOrigin,Id")] AirlineModel airlineModel)
        {
            if (id != airlineModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airlineModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirlineModelExists(airlineModel.Id))
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
            ViewData["CountryOfOrigin"] = new SelectList(_context.Locations, "Country", "Country", airlineModel.CountryOfOrigin);
            return View(airlineModel);
        }

        // GET: Airlines/Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Airlines == null)
            {
                return NotFound();
            }

            var airlineModel = await _context.Airlines
                .Include(a => a.LocationModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airlineModel == null)
            {
                return NotFound();
            }

            return View(airlineModel);
        }

        // POST: Airlines/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Airlines == null)
            {
                return Problem("Entity set 'DBContext.Airlines'  is null.");
            }
            var airlineModel = await _context.Airlines.FindAsync(id);
            if (airlineModel != null)
            {
                _context.Airlines.Remove(airlineModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirlineModelExists(Guid id)
        {
          return (_context.Airlines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

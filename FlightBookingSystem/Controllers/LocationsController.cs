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
    public class LocationsController : Controller
    {
        private readonly DBContext _context;

        public LocationsController(DBContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
              return _context.Locations != null ? 
                          View(await _context.Locations.ToListAsync()) :
                          Problem("Entity set 'DBContext.Locations'  is null.");
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var locationModel = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Country,City,Id")] LocationModel locationModel)
        {
            if (ModelState.IsValid)
            {
                locationModel.Id = Guid.NewGuid();
                _context.Add(locationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locationModel);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var locationModel = await _context.Locations.FindAsync(id);
            if (locationModel == null)
            {
                return NotFound();
            }
            return View(locationModel);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Country,City,Id")] LocationModel locationModel)
        {
            if (id != locationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationModelExists(locationModel.Id))
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
            return View(locationModel);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var locationModel = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locationModel == null)
            {
                return NotFound();
            }

            return View(locationModel);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Locations == null)
            {
                return Problem("Entity set 'DBContext.Locations'  is null.");
            }
            var locationModel = await _context.Locations.FindAsync(id);
            if (locationModel != null)
            {
                _context.Locations.Remove(locationModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationModelExists(Guid id)
        {
          return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

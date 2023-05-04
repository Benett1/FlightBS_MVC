using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem;
using FlightBookingSystem.Models;
using FlightBookingSystem.Models.DBGetModels;
namespace FlightBookingSystem.Controllers
{
    public class AirportsController : Controller
    {
        private readonly DBContext _context;

        public AirportsController(DBContext context)
        {
            _context = context;
        }

        // GET: Airports
        public async Task<IActionResult> Index()
        {
            if (GlobalState.UserRole == "Admin")
            {
                var dBContext = _context.Airports.Include(a => a.LocationModel);
                return View(await dBContext.ToListAsync());
            }
            return NotFound();
           
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Airports == null)
            {
                return NotFound();
            }

            var airportModel = await _context.Airports
                .Include(a => a.LocationModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportModel == null)
            {
                return NotFound();
            }

            return View(airportModel);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            ViewData["City"] = new SelectList(_context.Locations, "City", "City");
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String location, String name)
        {
            var city = _context.Locations.FirstOrDefault(e => e.City == location);
            AirportModel airportModel = new AirportModel();
            if (ModelState.IsValid)
            {
                airportModel.Id = Guid.NewGuid();
                airportModel.Name = name;
                airportModel.LocationID = city.Id;
                _context.Add(airportModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "Id", "Id", airportModel.LocationID);
            return View(airportModel);
        }

        // GET: Airports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Airports == null)
            {
                return NotFound();
            }

            var airportModel = await _context.Airports.FindAsync(id);
            if (airportModel == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Locations, "Id", "Id", airportModel.LocationID);
            return View(airportModel);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LocationID,Name,Id")] AirportModel airportModel)
        {
            if (id != airportModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airportModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportModelExists(airportModel.Id))
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
            ViewData["LocationID"] = new SelectList(_context.Locations, "Id", "Id", airportModel.LocationID);
            return View(airportModel);
        }

        // GET: Airports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Airports == null)
            {
                return NotFound();
            }

            var airportModel = await _context.Airports
                .Include(a => a.LocationModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airportModel == null)
            {
                return NotFound();
            }

            return View(airportModel);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Airports == null)
            {
                return Problem("Entity set 'DBContext.Airports'  is null.");
            }
            var airportModel = await _context.Airports.FindAsync(id);
            if (airportModel != null)
            {
                _context.Airports.Remove(airportModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportModelExists(Guid id)
        {
          return (_context.Airports?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

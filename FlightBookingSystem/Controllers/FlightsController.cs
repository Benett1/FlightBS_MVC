using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem;
using FlightBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlightBookingSystem.Controllers
{
    public class FlightsController : Controller
    {
        private readonly DBContext _context;

        public FlightsController(DBContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            if (GlobalState.UserRole == "Admin")
            {
                var dBContext = _context.Flights.Include(f => f.ArrivalAirportv).Include(f => f.DepartureAirportv).Include(f => f.PlaneModel);
                return View(await dBContext.ToListAsync());
            }
            return NotFound();
        }

        // GET: Flights/Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flightModel = await _context.Flights
                .Include(f => f.ArrivalAirportv)
                .Include(f => f.DepartureAirportv)
                .Include(f => f.PlaneModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flightModel == null)
            {
                return NotFound();
            }

            return View(flightModel);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["ArrivalAirport"] = new SelectList(_context.Airports, "Name", "Name");
            ViewData["DepartureAirport"] = new SelectList(_context.Airports, "Name", "Name");
            ViewData["PlaneId"] = new SelectList(_context.Planes, "Name", "Name");
            return View();
        }

        // POST: Flights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String planeId, string departureAirport, string arrivalAirport, DateTime dateTime)
        {
            FlightModel flightModel = new FlightModel();
            var plane = _context.Planes.FirstOrDefault(el => el.Name == planeId);
            var departure = _context.Airports.FirstOrDefault(el => el.Name == departureAirport);
            var arrival = _context.Airports.FirstOrDefault(el => el.Name == arrivalAirport);
            if (ModelState.IsValid)
            {
                flightModel.Id = Guid.NewGuid();
                flightModel.PlaneId = plane.Id;
                flightModel.DepartureAirport = departure.Id;
                flightModel.ArrivalAirport = arrival.Id;
                flightModel.DateTime = dateTime;
                _context.Add(flightModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArrivalAirport"] = new SelectList(_context.Airports, "Id", "Id", flightModel.ArrivalAirport);
            ViewData["DepartureAirport"] = new SelectList(_context.Airports, "Id", "Id", flightModel.DepartureAirport);
            ViewData["PlaneId"] = new SelectList(_context.Planes, "Id", "Id", flightModel.PlaneId);
            return View(flightModel);
        }

        // GET: Flights/Edit
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flightModel = await _context.Flights.FindAsync(id);
            if (flightModel == null)
            {
                return NotFound();
            }
            ViewData["ArrivalAirport"] = new SelectList(_context.Airports, "Id", "Id", flightModel.ArrivalAirport);
            ViewData["DepartureAirport"] = new SelectList(_context.Airports, "Id", "Id", flightModel.DepartureAirport);
            ViewData["PlaneId"] = new SelectList(_context.Planes, "Id", "Id", flightModel.PlaneId);
            return View(flightModel);
        }

        // POST: Flights/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PlaneId,DepartureAirport,ArrivalAirport,DateTime,Id")] FlightModel flightModel)
        {
            if (id != flightModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightModelExists(flightModel.Id))
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
            ViewData["ArrivalAirport"] = new SelectList(_context.Airports, "Id", "Id", flightModel.ArrivalAirport);
            ViewData["DepartureAirport"] = new SelectList(_context.Airports, "Id", "Id", flightModel.DepartureAirport);
            ViewData["PlaneId"] = new SelectList(_context.Planes, "Id", "Id", flightModel.PlaneId);
            return View(flightModel);
        }

        // GET: Flights/Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flightModel = await _context.Flights
                .Include(f => f.ArrivalAirportv)
                .Include(f => f.DepartureAirportv)
                .Include(f => f.PlaneModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flightModel == null)
            {
                return NotFound();
            }

            return View(flightModel);
        }

        // POST: Flights/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Flights == null)
            {
                return Problem("Entity set 'DBContext.Flight'  is null.");
            }
            var flightModel = await _context.Flights.FindAsync(id);
            if (flightModel != null)
            {
                _context.Flights.Remove(flightModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightModelExists(Guid id)
        {
          return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

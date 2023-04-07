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
    public class BookingsController : Controller
    {
        private readonly DBContext _context;

        public BookingsController(DBContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Bookings.Include(b => b.FlightModel).Include(b => b.UserModel);
            return View(await dBContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookingsModel = await _context.Bookings
                .Include(b => b.FlightModel)
                .Include(b => b.UserModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingsModel == null)
            {
                return NotFound();
            }

            return View(bookingsModel);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,UserId,Name,Surname,Age,Seat,baggage,Id")] BookingsModel bookingsModel)
        {
            if (ModelState.IsValid)
            {
                bookingsModel.Id = Guid.NewGuid();
                _context.Add(bookingsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", bookingsModel.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingsModel.UserId);
            return View(bookingsModel);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookingsModel = await _context.Bookings.FindAsync(id);
            if (bookingsModel == null)
            {
                return NotFound();
            }
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", bookingsModel.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingsModel.UserId);
            return View(bookingsModel);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FlightId,UserId,Name,Surname,Age,Seat,baggage,Id")] BookingsModel bookingsModel)
        {
            if (id != bookingsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsModelExists(bookingsModel.Id))
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", bookingsModel.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingsModel.UserId);
            return View(bookingsModel);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookingsModel = await _context.Bookings
                .Include(b => b.FlightModel)
                .Include(b => b.UserModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingsModel == null)
            {
                return NotFound();
            }

            return View(bookingsModel);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'DBContext.Bookings'  is null.");
            }
            var bookingsModel = await _context.Bookings.FindAsync(id);
            if (bookingsModel != null)
            {
                _context.Bookings.Remove(bookingsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsModelExists(Guid id)
        {
          return (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

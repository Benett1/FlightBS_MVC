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
            if (GlobalState.UserRole == "Admin")
            {
                var dBContext = _context.Bookings.Include(b => b.FlightModel).Include(b => b.UserModel);
                return View(await dBContext.ToListAsync());
            }
            else if (GlobalState.UserRole == "User") {
                var dBContext = await  _context.Bookings.Include(b => b.FlightModel).Include(b => b.UserModel).ToListAsync();
                var dbNew =  dBContext.Where(b => b.UserId == GlobalState.User.Id).ToList();
                return View(dbNew);
            }
            return NotFound();
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
        public IActionResult Create(string fid)
        {
            var itemList = new List<SelectListItem> {
                new SelectListItem { Value = fid, Text = fid }
            };

            FlightModel flight = _context.Flights
                .Single(f => f.Id == Guid.Parse(fid));

            PlaneModel plane = _context.Planes
                .Single(p => p.Id == flight.PlaneId);

            List<int> bookingIds = _context.Bookings
                .Where(b => b.FlightId == Guid.Parse(fid))
                .Select(b => b.Seat)
                .ToList();

            ViewData["FlightId"] = itemList;
            ViewData["UserId"] = GlobalState.User.Id;
            ViewData["BookingsForFlight"] = bookingIds;
            ViewData["Seats"] = plane.Seat;


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
                var id = GlobalState.User.Id;
                if (_context.Bookings.Where(b => b.Seat == bookingsModel.Seat).FirstOrDefault(b => b.Seat == bookingsModel.Seat) == null){
                    _context.Add(bookingsModel);
                    await _context.SaveChangesAsync();
                    ViewData["Success"] = "Data was saved successfully.";
                    return RedirectToAction(nameof(Index));
                }
            }
            var fid = bookingsModel.FlightId.ToString();
            var itemList = new List<SelectListItem> {
                new SelectListItem { Value = fid, Text = fid }
            };

            FlightModel flight = _context.Flights
                .Single(f => f.Id == Guid.Parse(fid));

            PlaneModel plane = _context.Planes
                .Single(p => p.Id == flight.PlaneId);

            List<int> bookingIds = _context.Bookings
                .Where(b => b.FlightId == Guid.Parse(fid))
                .Select(b => b.Seat)
                .ToList();

            ViewData["FlightId"] = itemList;
            ViewData["UserId"] = GlobalState.User.Id;
            ViewData["BookingsForFlight"] = bookingIds;
            ViewData["Seats"] = plane.Seat;
            ViewData["Success"] = "";

            return View("Create");
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
            ViewData["FlightId"] = new SelectList(_context.Flights, "Id", "Id", bookingsModel.FlightId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookingsModel.UserId);

            if (id != bookingsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (_context.Bookings.Where(b => b.Seat == bookingsModel.Seat).FirstOrDefault(b => b.Seat == bookingsModel.Seat) == null)
                    {
                        ViewData["Success"] = "Data was saved successfully.";
                        _context.Update(bookingsModel);
                        await _context.SaveChangesAsync();
                    }
                    else {
                        ViewData["Success"] = "";
                    }
                   
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
                return View(bookingsModel);
            }
            
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

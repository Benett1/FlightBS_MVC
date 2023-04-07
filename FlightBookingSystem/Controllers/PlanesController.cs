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
    public class PlanesController : Controller
    {
        private readonly DBContext _context;

        public PlanesController(DBContext context)
        {
            _context = context;
        }

        // GET: Planes
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.Planes.Include(p => p.AirlineModel);
            return View(await dBContext.ToListAsync());
        }

        // GET: Planes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Planes == null)
            {
                return NotFound();
            }

            var planeModel = await _context.Planes
                .Include(p => p.AirlineModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeModel == null)
            {
                return NotFound();
            }

            return View(planeModel);
        }

        // GET: Planes/Create
        public IActionResult Create()
        {
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "Id", "Id");
            return View();
        }

        // POST: Planes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Model,Manufacturer,Seat,Payload,Weight,AirlineId,Id")] PlaneModel planeModel)
        {
            if (ModelState.IsValid)
            {
                planeModel.Id = Guid.NewGuid();
                _context.Add(planeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "Id", "Id", planeModel.AirlineId);
            return View(planeModel);
        }

        // GET: Planes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Planes == null)
            {
                return NotFound();
            }

            var planeModel = await _context.Planes.FindAsync(id);
            if (planeModel == null)
            {
                return NotFound();
            }
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "Id", "Id", planeModel.AirlineId);
            return View(planeModel);
        }

        // POST: Planes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Model,Manufacturer,Seat,Payload,Weight,AirlineId,Id")] PlaneModel planeModel)
        {
            if (id != planeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaneModelExists(planeModel.Id))
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
            ViewData["AirlineId"] = new SelectList(_context.Airlines, "Id", "Id", planeModel.AirlineId);
            return View(planeModel);
        }

        // GET: Planes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Planes == null)
            {
                return NotFound();
            }

            var planeModel = await _context.Planes
                .Include(p => p.AirlineModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planeModel == null)
            {
                return NotFound();
            }

            return View(planeModel);
        }

        // POST: Planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Planes == null)
            {
                return Problem("Entity set 'DBContext.Plane'  is null.");
            }
            var planeModel = await _context.Planes.FindAsync(id);
            if (planeModel != null)
            {
                _context.Planes.Remove(planeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaneModelExists(Guid id)
        {
          return (_context.Planes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

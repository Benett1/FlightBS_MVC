using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem;
using FlightBookingSystem.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace FlightBookingSystem.Controllers
{
    public class UsersController : Controller
    {

        private readonly DBContext _context;

        public UsersController(DBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var userid = Request.Cookies["userId"];

            var userModel = await _context.Users
               .Include(u => u.RoleModel)
               .FirstOrDefaultAsync(m => ""+m.Id == userid);

            GlobalState.UserRole = userModel != null ? userModel.RoleModel.RoleName : "Guest";
            return View(userModel);
        }

        public IActionResult Logout() {
            Response.Cookies.Delete("userId");
            GlobalState.User = null;
            return RedirectToAction("Index","Home");
        }

        // GET: Users/Details
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .Include(u => u.RoleModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        public async Task<IActionResult> Login(String name, String password)
        {
            if (name == null || password == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .Include(u => u.RoleModel)
                .FirstOrDefaultAsync(m => m.Name == name && m.Password == password);


            if (userModel == null)
            {
                return RedirectToAction("Index");
            }
            else {
                Response.Cookies.Append("userId", userModel.Id.ToString(), new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    Secure = true
                });
                GlobalState.User = userModel;
                GlobalState.UserRole = userModel.RoleModel!.RoleName!;

                
            }
            return RedirectToAction("Index","Home");
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string surname, int age, string password)
        {
            UserModel userModel = new UserModel();
            var role = _context.Roles.FirstOrDefault(c => c.RoleName == "User");

            if (ModelState.IsValid)
            {
                userModel.Id = Guid.NewGuid();
                userModel.Name = name;
                userModel.Surname = surname;
                userModel.Age = age;
                userModel.Password = password;
                userModel.RoleId = role.Id; 
                
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Name"] = new SelectList(_context.Roles, "Id", "Id", userModel.RoleId);
            return View(userModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", userModel.RoleId);
            return View(userModel);
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Surname,Age,Password,RoleId,Id")] UserModel userModel)
        {
            if (id != userModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", userModel.RoleId);
            return View(userModel);
        }

        // GET: Users/Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var userModel = await _context.Users
                .Include(u => u.RoleModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DBContext.User'  is null.");
            }
            var userModel = await _context.Users.FindAsync(id);
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
                Response.Cookies.Delete("userId");
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(Guid id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

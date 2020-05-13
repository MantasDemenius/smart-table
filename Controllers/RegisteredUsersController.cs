using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_table.Models;
using smart_table.Models.DataBase;

namespace smart_table.Controllers
{
    public class RegisteredUsersController : Controller
    {
        private readonly DataBaseContext _context;

        public RegisteredUsersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: RegisteredUsers
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.RegisteredUsers.Include(r => r.RoleNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: RegisteredUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registeredUsers = await _context.RegisteredUsers
                .Include(r => r.RoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registeredUsers == null)
            {
                return NotFound();
            }

            return View(registeredUsers);
        }

        // GET: RegisteredUsers/Create
        public IActionResult Create()
        {
            ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name");
            return View();
        }

        // POST: RegisteredUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Password,Phone,Email,BirthDate,IsBlocked,Role")] RegisteredUsers registeredUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registeredUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
            return View(registeredUsers);
        }

        // GET: RegisteredUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registeredUsers = await _context.RegisteredUsers.FindAsync(id);
            if (registeredUsers == null)
            {
                return NotFound();
            }
            ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
            return View(registeredUsers);
        }

        // POST: RegisteredUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Surname,Password,Phone,Email,BirthDate,IsBlocked,Role")] RegisteredUsers registeredUsers)
        {
            if (id != registeredUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registeredUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisteredUsersExists(registeredUsers.Id))
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
            ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
            return View(registeredUsers);
        }

        // GET: RegisteredUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registeredUsers = await _context.RegisteredUsers
                .Include(r => r.RoleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registeredUsers == null)
            {
                return NotFound();
            }

            return View(registeredUsers);
        }

        // POST: RegisteredUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var registeredUsers = await _context.RegisteredUsers.FindAsync(id);
            _context.RegisteredUsers.Remove(registeredUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisteredUsersExists(long id)
        {
            return _context.RegisteredUsers.Any(e => e.Id == id);
        }
    }
}

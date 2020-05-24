using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_table.Models;
using smart_table.Models.DataBase;

namespace smart_table.Controllers
{
    
    public class ManageRegisteredUsersController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "~/Staff/Views/";

        public ManageRegisteredUsersController(DataBaseContext context)
        {
            _context = context;
        }

        [Route("ManageRegisteredUsers")]
        public async Task<IActionResult> OpenManageRegisteredUsersView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            var dataBaseContext = _context.RegisteredUsers.Include(r => r.RoleNavigation);
            return View(_viewsPath + "ManageRegisteredUsersView.cshtml", await dataBaseContext.ToListAsync());
        }

        public async Task<IActionResult> OpenRegisteredUserView(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
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

            return View(_viewsPath + "RegisteredUserView.cshtml", registeredUsers);
        }

        public IActionResult OpenCreateRegisteredUserView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name");
            return View(_viewsPath + "RegisteredUserCreateForm.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRegisteredUser([Bind("Id,Name,Surname,Password,Phone,Email,BirthDate,IsBlocked,Role")] RegisteredUsers registeredUsers)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            if (!ModelState.IsValid)
            {
                ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
                return View(_viewsPath + "RegisteredUserCreateForm.cshtml", registeredUsers);
            }
            if (!ValidateRegisteredUser(registeredUsers))
            {
                ViewData["message"] = "Netinkama gimimo data";
                ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
                return View(_viewsPath + "RegisteredUserCreateForm.cshtml", registeredUsers);
            }
            _context.Add(registeredUsers);
            await _context.SaveChangesAsync();
            ViewData["message"] = "Naudotojas buvo sukurtas sėkmingai";
            var dataBaseContext = _context.RegisteredUsers.Include(r => r.RoleNavigation);
            return View(_viewsPath + "ManageRegisteredUsersView.cshtml", await dataBaseContext.ToListAsync());
        }

        private bool ValidateRegisteredUser(RegisteredUsers user)
        {
            if (user.BirthDate > DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public async Task<IActionResult> OpenEditRegisteredUserView(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
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
            return View(_viewsPath + "RegisteredUserEditForm.cshtml" , registeredUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRegisteredUser(long id, [Bind("Id,Name,Surname,Password,Phone,Email,BirthDate,IsBlocked,Role")] RegisteredUsers registeredUsers)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            if (id != registeredUsers.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
                return View(_viewsPath + "RegisteredUserEditForm.cshtml",registeredUsers);
            }
            if (!ValidateRegisteredUser(registeredUsers))
            {
                ViewData["message"] = "Netinkama gimimo data";
                ViewData["Role"] = new SelectList(_context.UserRole, "Id", "Name", registeredUsers.Role);
                return View(_viewsPath + "RegisteredUserEditForm.cshtml", registeredUsers);
            }
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
            ViewData["message"] = "Naudotojo duomenys pakeisti sėkmingai";
            var dataBaseContext = _context.RegisteredUsers.Include(r => r.RoleNavigation);
            return View(_viewsPath + "ManageRegisteredUsersView.cshtml", await dataBaseContext.ToListAsync());

        }

        public async Task<IActionResult> DeleteRegisteredUser(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
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

            return View(_viewsPath + "RegisteredUserDeleteView.cshtml", registeredUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRegisteredUser(long id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            var registeredUsers = await _context.RegisteredUsers.FindAsync(id);
            _context.RegisteredUsers.Remove(registeredUsers);
            await _context.SaveChangesAsync();
            ViewData["message"] = "Naudotojas pašalintas sėkmingai";
            var dataBaseContext = _context.RegisteredUsers.Include(r => r.RoleNavigation);
            return View(_viewsPath + "ManageRegisteredUsersView.cshtml", await dataBaseContext.ToListAsync());
        }

        private bool RegisteredUsersExists(long id)
        {
            return _context.RegisteredUsers.Any(e => e.Id == id);
        }
    }
}

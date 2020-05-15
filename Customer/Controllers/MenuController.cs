using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_table.Models;
using smart_table.Models.DataBase;

namespace smart_table.Customer.Controllers
{
    public class MenuController : Controller
    {
        private readonly DataBaseContext _context;

        public MenuController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.MenuDishes.Include(m => m.FkDishesNavigation).Include(m => m.FkMenusNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuDishes = await _context.MenuDishes
                .Include(m => m.FkDishesNavigation)
                .Include(m => m.FkMenusNavigation)
                .FirstOrDefaultAsync(m => m.FkDishes == id);
            if (menuDishes == null)
            {
                return NotFound();
            }

            return View(menuDishes);
        }

        // GET: Menu/Create
        public IActionResult Create()
        {
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title");
            ViewData["FkMenus"] = new SelectList(_context.Menus, "Id", "Title");
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateFrom,DateUntil,FkDishes,FkMenus")] MenuDishes menuDishes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuDishes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title", menuDishes.FkDishes);
            ViewData["FkMenus"] = new SelectList(_context.Menus, "Id", "Title", menuDishes.FkMenus);
            return View(menuDishes);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuDishes = await _context.MenuDishes.FindAsync(id);
            if (menuDishes == null)
            {
                return NotFound();
            }
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title", menuDishes.FkDishes);
            ViewData["FkMenus"] = new SelectList(_context.Menus, "Id", "Title", menuDishes.FkMenus);
            return View(menuDishes);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("DateFrom,DateUntil,FkDishes,FkMenus")] MenuDishes menuDishes)
        {
            if (id != menuDishes.FkDishes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuDishes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuDishesExists(menuDishes.FkDishes))
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
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title", menuDishes.FkDishes);
            ViewData["FkMenus"] = new SelectList(_context.Menus, "Id", "Title", menuDishes.FkMenus);
            return View(menuDishes);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuDishes = await _context.MenuDishes
                .Include(m => m.FkDishesNavigation)
                .Include(m => m.FkMenusNavigation)
                .FirstOrDefaultAsync(m => m.FkDishes == id);
            if (menuDishes == null)
            {
                return NotFound();
            }

            return View(menuDishes);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var menuDishes = await _context.MenuDishes.FindAsync(id);
            _context.MenuDishes.Remove(menuDishes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuDishesExists(long id)
        {
            return _context.MenuDishes.Any(e => e.FkDishes == id);
        }
    }
}

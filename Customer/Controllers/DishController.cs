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

namespace smart_table.Customer.Controllers
{
    public class DishController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "~/Customer/Views/Dish/";

        public DishController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Dish
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Dishes.Include(d => d.FkDishCategoriesNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Dish/Details/5
        public async Task<IActionResult> openDishView(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes
                .Include(d => d.FkDishCategoriesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishes == null)
            {
                return NotFound();
            }

            return View(_viewsPath + "DishView.cshtml", dishes);
        }

        // GET: Dish/Create
        public IActionResult Create()
        {
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title");
            return View();
        }

        // POST: Dish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,Calories,Discount,FkDishCategories")] Dishes dishes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title", dishes.FkDishCategories);
            return View(dishes);
        }

        // GET: Dish/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes.FindAsync(id);
            if (dishes == null)
            {
                return NotFound();
            }
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title", dishes.FkDishCategories);
            return View(dishes);
        }

        // POST: Dish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Description,Price,Calories,Discount,FkDishCategories")] Dishes dishes)
        {
            if (id != dishes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishesExists(dishes.Id))
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
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title", dishes.FkDishCategories);
            return View(dishes);
        }

        // GET: Dish/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes
                .Include(d => d.FkDishCategoriesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishes == null)
            {
                return NotFound();
            }

            return View(dishes);
        }

        // POST: Dish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var dishes = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dishes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishesExists(long id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}

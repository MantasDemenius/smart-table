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
    public class RecomendationController : Controller
    {
        private readonly DataBaseContext _context;

        public RecomendationController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Recomendation
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Dishes.Include(d => d.FkDishCategoriesNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Recomendation/Details/5
        public async Task<IActionResult> Details(long? id)
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

        // GET: Recomendation/Create
        public IActionResult Create()
        {
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title");
            return View();
        }

        // POST: Recomendation/Create
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

        // GET: Recomendation/Edit/5
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

        // POST: Recomendation/Edit/5
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

        // GET: Recomendation/Delete/5
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

        // POST: Recomendation/Delete/5
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

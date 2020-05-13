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
    public class DishCategoriesController : Controller
    {
        private readonly DataBaseContext _context;

        public DishCategoriesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: DishCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.DishCategories.ToListAsync());
        }

        // GET: DishCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategories = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishCategories == null)
            {
                return NotFound();
            }

            return View(dishCategories);
        }

        // GET: DishCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DishCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] DishCategories dishCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishCategories);
        }

        // GET: DishCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategories = await _context.DishCategories.FindAsync(id);
            if (dishCategories == null)
            {
                return NotFound();
            }
            return View(dishCategories);
        }

        // POST: DishCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title")] DishCategories dishCategories)
        {
            if (id != dishCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishCategoriesExists(dishCategories.Id))
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
            return View(dishCategories);
        }

        // GET: DishCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategories = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishCategories == null)
            {
                return NotFound();
            }

            return View(dishCategories);
        }

        // POST: DishCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var dishCategories = await _context.DishCategories.FindAsync(id);
            _context.DishCategories.Remove(dishCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishCategoriesExists(long id)
        {
            return _context.DishCategories.Any(e => e.Id == id);
        }
    }
}

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
    public class ManageOrderController : Controller
    {
        private readonly DataBaseContext _context;

        public ManageOrderController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: ManageOrder
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.OrderDishes.Include(o => o.FkDishesNavigation).Include(o => o.FkOrdersNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: ManageOrder/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDishes = await _context.OrderDishes
                .Include(o => o.FkDishesNavigation)
                .Include(o => o.FkOrdersNavigation)
                .FirstOrDefaultAsync(m => m.FkDishes == id);
            if (orderDishes == null)
            {
                return NotFound();
            }

            return View(orderDishes);
        }

        // GET: ManageOrder/Create
        public IActionResult Create()
        {
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title");
            ViewData["FkOrders"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: ManageOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,Comment,FkOrders,FkDishes")] OrderDishes orderDishes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDishes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title", orderDishes.FkDishes);
            ViewData["FkOrders"] = new SelectList(_context.Orders, "Id", "Id", orderDishes.FkOrders);
            return View(orderDishes);
        }

        // GET: ManageOrder/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDishes = await _context.OrderDishes.FindAsync(id);
            if (orderDishes == null)
            {
                return NotFound();
            }
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title", orderDishes.FkDishes);
            ViewData["FkOrders"] = new SelectList(_context.Orders, "Id", "Id", orderDishes.FkOrders);
            return View(orderDishes);
        }

        // POST: ManageOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Quantity,Comment,FkOrders,FkDishes")] OrderDishes orderDishes)
        {
            if (id != orderDishes.FkDishes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDishes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDishesExists(orderDishes.FkDishes))
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
            ViewData["FkDishes"] = new SelectList(_context.Dishes, "Id", "Title", orderDishes.FkDishes);
            ViewData["FkOrders"] = new SelectList(_context.Orders, "Id", "Id", orderDishes.FkOrders);
            return View(orderDishes);
        }

        // GET: ManageOrder/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDishes = await _context.OrderDishes
                .Include(o => o.FkDishesNavigation)
                .Include(o => o.FkOrdersNavigation)
                .FirstOrDefaultAsync(m => m.FkDishes == id);
            if (orderDishes == null)
            {
                return NotFound();
            }

            return View(orderDishes);
        }

        // POST: ManageOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var orderDishes = await _context.OrderDishes.FindAsync(id);
            _context.OrderDishes.Remove(orderDishes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDishesExists(long id)
        {
            return _context.OrderDishes.Any(e => e.FkDishes == id);
        }
    }
}

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
    public class BillsController : Controller
    {
        private readonly DataBaseContext _context;

        public BillsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Bills.Include(b => b.FkDiscountsNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _context.Bills
                .Include(b => b.FkDiscountsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bills == null)
            {
                return NotFound();
            }

            return View(bills);
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode");
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,Tips,Amount,IsPaid,Evaluation,FkDiscounts")] Bills bills)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode", bills.FkDiscounts);
            return View(bills);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _context.Bills.FindAsync(id);
            if (bills == null)
            {
                return NotFound();
            }
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode", bills.FkDiscounts);
            return View(bills);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DateTime,Tips,Amount,IsPaid,Evaluation,FkDiscounts")] Bills bills)
        {
            if (id != bills.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bills);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillsExists(bills.Id))
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
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode", bills.FkDiscounts);
            return View(bills);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bills = await _context.Bills
                .Include(b => b.FkDiscountsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bills == null)
            {
                return NotFound();
            }

            return View(bills);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var bills = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bills);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillsExists(long id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}

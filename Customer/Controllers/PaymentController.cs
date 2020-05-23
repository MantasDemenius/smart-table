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
    public class PaymentController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "~/Customer/Views/Payment/";

        public PaymentController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Bills.Include(b => b.FkDiscountsNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Payment/Details/5
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

        // GET: Payment/Create
        public IActionResult openPaymentView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode");
            return View(_viewsPath + "Payment.cshtml");
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> openPaymentView([Bind("Id,DateTime,Tips,Amount,IsPaid,Evaluation,FkDiscounts,FkCustomerTables")] Bills bills)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            if (ModelState.IsValid)
            {
                _context.Add(bills);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode", bills.FkDiscounts);
            return View(_viewsPath + "Payment.cshtml", bills);
        }

        // GET: Payment/Edit/5
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

        // POST: Payment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DateTime,Tips,Amount,IsPaid,Evaluation,FkDiscounts,FkCustomerTables")] Bills bills)
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

        // GET: Payment/Delete/5
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

        // POST: Payment/Delete/5
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

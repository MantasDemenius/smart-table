using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            var tableId = HttpContext.Session.GetInt32("customer_table_id");
            if (tableId == null)
            {
                return NotFound();
            }
            
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode");

            var bill = _context.Bills
                .FirstOrDefault(b => b.FkCustomerTables == tableId && b.IsPaid == false);

            if (bill == null)
            {
                return NotFound();
            }

            var billOrders = _context.OrderDishes
                .Include(d => d.FkDishesNavigation)
                .Include(o => o.FkOrdersNavigation)
                .Where(dob => dob.FkOrdersNavigation.FkBills == bill.Id)
                .Select(s => new
                {
                    Price = Math.Round((double)(s.FkDishesNavigation.Price - (s.FkDishesNavigation.Price * (s.FkDishesNavigation.Discount / 100))) * s.Quantity, 2)
                }).ToList();
            //double discountedPrice = Math.Round((double)(item.FkDishesNavigation.Price - (item.FkDishesNavigation.Price * (item.FkDishesNavigation.Discount / 100))), 2);
            var amount = billOrders.Aggregate(0.0, (acc, x) => acc + x.Price);

            bill.Amount = amount;

            return View(_viewsPath + "Payment.cshtml", bill);
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> submitPayment([Bind("Id,DateTime,Tips,Amount,IsPaid,Evaluation,FkDiscounts,FkCustomerTables")] Bills bills)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");

            if (ModelState.IsValid && validatePayment(bills))
            {
                try
                {
                    Events events = new Events();
                    events.Type = 1;
                    events.FkBills = bills.Id;
                    _context.Add(events);
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
                ViewData["message"] = "Padavėjas buvo pakviestas, palaukite";
                return View(_viewsPath + "Payment.cshtml", bills);
            }

            ViewData["message"] = "Prastai užpildyti laukai";
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode", bills.FkDiscounts);
            return View(_viewsPath + "Payment.cshtml", bills);
        }

        private bool validatePayment(Bills bill)
        {
            return bill.IsPaid == false && bill.Amount > 0 && bill.Tips >= 0;
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

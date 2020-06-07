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

            if (billOrders.Count == 0)
            {
                var dataBaseContext = _context.MenuDishes.Include(m => m.FkDishesNavigation).Include(m => m.FkMenusNavigation);
                ViewData["message"] = "Jūsų krepšelis tuščias";
                return View("~/Customer/Views/Menu/" + "MainMenuView.cshtml", dataBaseContext.ToList());
            }
            //double discountedPrice = Math.Round((double)(item.FkDishesNavigation.Price - (item.FkDishesNavigation.Price * (item.FkDishesNavigation.Discount / 100))), 2);
            var amount = Math.Round(billOrders.Aggregate(0.0, (acc, x) => acc + x.Price),2);

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
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");

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

            ViewData["message"] = "Įvyko klaida, pakvieskite padavėją";
            ViewData["FkDiscounts"] = new SelectList(_context.Discounts, "Id", "DiscountCode", bills.FkDiscounts);
            return View(_viewsPath + "Payment.cshtml", bills);
        }

        private bool validatePayment(Bills bill)
        {
            return bill.IsPaid == false && bill.Amount > 0 && bill.Tips >= 0;
        }

        private bool BillsExists(long id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}

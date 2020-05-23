using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private readonly HydrometereologyInterface _hydrometereologyInterface;
        private string _viewsPath = "~/Customer/Views/ManageOrder/";

        public ManageOrderController(DataBaseContext context)
        {
            _hydrometereologyInterface = new HydrometereologyInterface();
            _context = context;
        }

        // GET: ManageOrder
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.OrderDishes.Include(o => o.FkDishesNavigation).Include(o => o.FkOrdersNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        public IActionResult openOrderDishForm(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            HttpContext.Session.SetInt32("dish_id", Convert.ToInt32(id));
            return View(_viewsPath + "OrderDishFormView.cshtml");
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

        [HttpPost]
        public async Task<IActionResult> addDish(int quantity, string comment)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            long dishId = (long)HttpContext.Session.GetInt32("dish_id");
            if (validateOrderDish(quantity)) {
                double temperature = _hydrometereologyInterface.GetTemperature();
                if (HttpContext.Session.GetInt32("order_id") == null) {
                    Orders order = new Orders();
                    order.FkBillsNavigation.FkCustomerTables = (long)HttpContext.Session.GetInt32("customer_table_id");
                    order.FkBills = (long)HttpContext.Session.GetInt32("bill_id");
                    order.Temperature = temperature;
                    _context.Add(order);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("order_id", Convert.ToInt32(order.Id));
                }
                OrderDishes orderDish = new OrderDishes();
                OrderDishes tempDish = _context.OrderDishes
                    .Where(a => a.FkOrders == (long)HttpContext.Session.GetInt32("order_id"))
                    .Where(b => b.FkDishes == dishId).FirstOrDefault();

                if (tempDish == null) {
                    orderDish.Quantity = quantity;
                    orderDish.Comment = comment;
                    orderDish.FkOrders = (long)HttpContext.Session.GetInt32("order_id");
                    orderDish.FkDishes = dishId;

                    _context.Add(orderDish);
                } else {
                    tempDish.Quantity += quantity;
                    tempDish.Comment = comment;
                    _context.Entry(tempDish).State = EntityState.Modified;
                }
                    
                await _context.SaveChangesAsync();
                return Redirect("~/Menu");

            } else {
                ViewData["message"] = "Kiekis turi būti didesnis už 0";
                return View(_viewsPath + "OrderDishFormView.cshtml");
            }
        }

        private bool validateOrderDish(int quantity) {
            return quantity > 0;
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

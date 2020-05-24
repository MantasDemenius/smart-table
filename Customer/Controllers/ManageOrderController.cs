using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            HttpContext.Session.SetInt32("dish_id", Convert.ToInt32(id));
            return View(_viewsPath + "OrderDishFormView.cshtml");
        }

        public IActionResult openManageOrderView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            var tableId = HttpContext.Session.GetInt32("customer_table_id");

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
                    id = s.FkOrders,
                    title = s.FkDishesNavigation.Title,
                    quantity = s.Quantity,
                    price = Math.Round((double)(s.FkDishesNavigation.Price - (s.FkDishesNavigation.Price * (s.FkDishesNavigation.Discount / 100))) * s.Quantity, 2)
                }).ToList();

            double amount = billOrders.Aggregate(0.0, (acc, x) => acc + x.price);

            if (billOrders.Count == 0)
            {
                var dataBaseContext = _context.MenuDishes.Include(m => m.FkDishesNavigation).Include(m => m.FkMenusNavigation);
                ViewData["message"] = "Jūsų krepšelis tuščias";
                return View("~/Customer/Views/Menu/" + "MainMenuView.cshtml", dataBaseContext.ToList());
            }

            TempData["amount"] = Math.Round(amount, 2);
            TempData["id"] = billOrders.First().id;

            dynamic BillModel = new ExpandoObject();
            BillModel.Orders = billOrders;

            return View(_viewsPath + "ManageOrderView.cshtml", BillModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitOrder(long id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            var tableId = HttpContext.Session.GetInt32("customer_table_id");

            var bill = _context.Bills
                .FirstOrDefault(b => b.FkCustomerTables == tableId && b.IsPaid == false);

            var order = _context.Orders
                .FirstOrDefault(o => o.Id == id);

            var billOrders = _context.OrderDishes
             .Include(d => d.FkDishesNavigation)
             .Include(o => o.FkOrdersNavigation)
             .Where(dob => dob.FkOrdersNavigation.FkBills == bill.Id)
             .Select(s => new
             {
                 id = s.FkOrders,
                 title = s.FkDishesNavigation.Title,
                 quantity = s.Quantity,
                 price = Math.Round((double)(s.FkDishesNavigation.Price - (s.FkDishesNavigation.Price * (s.FkDishesNavigation.Discount / 100))) * s.Quantity, 2)
             }).ToList();

            try
                {
                    order.Submitted = true;
                    Events events = new Events();
                    events.Type = 4;
                    events.FkBills = (long)order.FkBills;
                    _context.Add(events);
                    _context.Update(order);
                     _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDishesExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            double amount = billOrders.Aggregate(0.0, (acc, x) => acc + x.price);


            TempData["amount"] = Math.Round(amount, 2);
            TempData["id"] = billOrders.First().id;


            dynamic BillModel = new ExpandoObject();
            BillModel.Orders = billOrders;

            ViewData["message"] = "Jūsų užsakymas buvo priimtas";

            return View(_viewsPath + "ManageOrderView.cshtml", BillModel);
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

        public async Task<IActionResult> goBack() {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            long dishId = (long)HttpContext.Session.GetInt32("dish_id");
            var dishes = await _context.Dishes
                .Include(d => d.FkDishCategoriesNavigation)
                .FirstOrDefaultAsync(m => m.Id == dishId);
            if (dishes == null)
            {
                return NotFound();
            }
            return View("~/Customer/Views/Dish/DishView.cshtml", dishes);
        }
    }
}

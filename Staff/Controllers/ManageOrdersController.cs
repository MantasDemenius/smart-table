using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_table.Models;
using smart_table.Models.DataBase;

namespace smart_table.Staff.Controllers
{
    public class ManageOrdersController : Controller
    {
        private readonly DataBaseContext _context;

        public ManageOrdersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: ManageOrders
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Orders.Include(o => o.FkBillsNavigation).Include(o => o.FkCustomerTablesNavigation).Include(o => o.FkRegisteredUsersNavigation);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: ManageOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.FkBillsNavigation)
                .Include(o => o.FkCustomerTablesNavigation)
                .Include(o => o.FkRegisteredUsersNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: ManageOrders/Create
        public IActionResult Create()
        {
            ViewData["FkBills"] = new SelectList(_context.Bills, "Id", "Evaluation");
            ViewData["FkCustomerTables"] = new SelectList(_context.CustomerTables, "Id", "Id");
            ViewData["FkRegisteredUsers"] = new SelectList(_context.RegisteredUsers, "Id", "Email");
            return View();
        }

        // POST: ManageOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,Temperature,Submitted,Served,FkBills,FkRegisteredUsers,FkCustomerTables")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkBills"] = new SelectList(_context.Bills, "Id", "Evaluation", orders.FkBills);
            ViewData["FkCustomerTables"] = new SelectList(_context.CustomerTables, "Id", "Id", orders.FkCustomerTables);
            ViewData["FkRegisteredUsers"] = new SelectList(_context.RegisteredUsers, "Id", "Email", orders.FkRegisteredUsers);
            return View(orders);
        }

        // GET: ManageOrders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["FkBills"] = new SelectList(_context.Bills, "Id", "Evaluation", orders.FkBills);
            ViewData["FkCustomerTables"] = new SelectList(_context.CustomerTables, "Id", "Id", orders.FkCustomerTables);
            ViewData["FkRegisteredUsers"] = new SelectList(_context.RegisteredUsers, "Id", "Email", orders.FkRegisteredUsers);
            return View(orders);
        }

        // POST: ManageOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DateTime,Temperature,Submitted,Served,FkBills,FkRegisteredUsers,FkCustomerTables")] Orders orders)
        {
            if (id != orders.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.Id))
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
            ViewData["FkBills"] = new SelectList(_context.Bills, "Id", "Evaluation", orders.FkBills);
            ViewData["FkCustomerTables"] = new SelectList(_context.CustomerTables, "Id", "Id", orders.FkCustomerTables);
            ViewData["FkRegisteredUsers"] = new SelectList(_context.RegisteredUsers, "Id", "Email", orders.FkRegisteredUsers);
            return View(orders);
        }

        // GET: ManageOrders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.FkBillsNavigation)
                .Include(o => o.FkCustomerTablesNavigation)
                .Include(o => o.FkRegisteredUsersNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: ManageOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

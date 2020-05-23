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

namespace smart_table.Staff.Controllers
{
    public class AssignTableController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "Staff/Views/";

        public AssignTableController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: AssignTable
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerTables.ToListAsync());
        }

        // GET: AssignTable/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTables = await _context.CustomerTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerTables == null)
            {
                return NotFound();
            }

            return View(customerTables);
        }

        // GET: AssignTable/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult BackToPrevious()
        {            
            return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
        }

        // POST: AssignTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeatsCount,QrCode,IsTaken,JoinCode")] CustomerTables customerTables)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerTables);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerTables);
        }

        // GET: AssignTable/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTables = await _context.CustomerTables.FindAsync(id);
            if (customerTables == null)
            {
                return NotFound();
            }
            return View(customerTables);
        }

        // POST: AssignTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SeatsCount,QrCode,IsTaken,JoinCode")] CustomerTables customerTables)
        {
            if (id != customerTables.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerTables);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerTablesExists(customerTables.Id))
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
            return View(customerTables);
        }

        // GET: AssignTable/Delete/5
        public async Task<IActionResult> AssignTable(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            if (id == null)
            {
                return NotFound();
            }

            var customerTables = await _context.CustomerTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerTables == null)
            {
                return NotFound();
            }

            return View(_viewsPath + "AssignTableConfirmView.cshtml", customerTables);
        }

        // POST: AssignTable/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTable(long id)
        {
            
            var customerTables = await _context.CustomerTables.FindAsync(id);
            var bills = await _context.Bills
                .Include(o => o.Orders)
                .Where(b => b.IsPaid == false && b.FkCustomerTables == id).ToListAsync();
            if(bills.Count == 0)
            {
                HttpContext.Session.SetString("message", "Priskyrimas nepavyko - šis stalas neturi aktyvių sąskaitų...");
                return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
            }
            List<Orders> orders = new List<Orders>();
            foreach(var bill in bills)
            {
                foreach(var order in bill.Orders)
                {
                    orders.Add(order);
                }
            }
            if (orders.Count == 0)
            {
                HttpContext.Session.SetString("message", "Priskyrimas nepavyko - šis stalas neturi užsakymų...");
                return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
            }
            List<Orders> orders_to_assign = new List<Orders>();
            foreach(var order in orders)
            {
                if (order.FkRegisteredUsers == null)
                    orders_to_assign.Add(order);
            }
            if (orders_to_assign.Count == 0)
            {
                HttpContext.Session.SetString("message", "Priskyrimas nepavyko - visi šio stalo užsakymai priskirti...");
                return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
            }
            foreach(var order in orders_to_assign)
            {
                order.FkRegisteredUsers = HttpContext.Session.GetInt32("user_id");
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            HttpContext.Session.SetString("message", "Laisvi stalo užsakymai sėkmingai priskirti!");
            return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
        }

        private bool CustomerTablesExists(long id)
        {
            return _context.CustomerTables.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class QrCodeCustomerController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "~/Customer/Views/QrCodeCustomer/";

        public QrCodeCustomerController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: OccupyTable/1
        [Route("TakeTable")]
        public async Task<IActionResult> occupyTable(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetInt32("user_role") != (int)3) {
                HttpContext.Session.SetInt32("customer_table_id", Convert.ToInt32(id));
                string joinCode = generateJoinCode();
                HttpContext.Session.SetString("table_code", joinCode);

                var customerTable = await _context.CustomerTables.FindAsync(id);
                if (customerTable == null || customerTable.IsTaken) {
                    return View(_viewsPath + "QrCodeView.cshtml");
                }

                customerTable.JoinCode = joinCode;
                customerTable.IsTaken = true;
                _context.Entry(customerTable).State = EntityState.Modified;

                var bill = new Bills();
                bill.Evaluation = "";

                _context.Add(bill);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("bill_id", Convert.ToInt32(bill.Id));
            }
            
            HttpContext.Session.SetInt32("user_role", 3);
            return Redirect("~/Menu");
        }

        private string generateJoinCode() {
            Random rnd = new Random();
            return rnd.Next(1000, 10000).ToString();
        }

        // GET: QrCodeCustomer
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerTables.ToListAsync());
        }

        // GET: QrCodeCustomer/Details/5
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

        // GET: QrCodeCustomer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QrCodeCustomer/Create
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

        // GET: QrCodeCustomer/Edit/5
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

        // POST: QrCodeCustomer/Edit/5
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

        // GET: QrCodeCustomer/Delete/5
        public async Task<IActionResult> Delete(long? id)
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

        // POST: QrCodeCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customerTables = await _context.CustomerTables.FindAsync(id);
            _context.CustomerTables.Remove(customerTables);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerTablesExists(long id)
        {
            return _context.CustomerTables.Any(e => e.Id == id);
        }
    }
}

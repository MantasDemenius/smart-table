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
    public class ViewTablesController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "Staff/Views/";

        public ViewTablesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: ViewTables
        [Route("ViewTables")]
        public async Task<IActionResult> OpenViewTablesView()
        {
            ViewData["message"] = HttpContext.Session.GetString("message");
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            HttpContext.Session.SetString("message", "");
            HttpContext.Session.SetString("previous_page", "ViewTables");


            
            var dataTuple = new Tuple<List<CustomerTables>, Byte[]>(await _context.CustomerTables.ToListAsync(), null);
            return View(_viewsPath + "TableListView.cshtml", dataTuple);
        }

        // GET: ViewTables/Details/5
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

        // GET: ViewTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViewTables/Create
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

        // GET: ViewTables/Edit/5
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

        // POST: ViewTables/Edit/5
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

        // GET: ViewTables/Delete/5
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

        // POST: ViewTables/Delete/5
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

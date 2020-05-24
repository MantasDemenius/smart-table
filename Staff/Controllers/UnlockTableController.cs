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
    public class UnlockTableController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "Staff/Views/";

        public UnlockTableController(DataBaseContext context)
        {
            _context = context;
        }

        public IActionResult BackToPrevious()
        {
            return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
        }
   
        public async Task<IActionResult> UnlockTable(long? id)
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

            return View(_viewsPath + "UnlockTableConfirmView.cshtml", customerTables);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockTable(long id)
        {
            var customerTables = await _context.CustomerTables.FindAsync(id);
            customerTables.IsTaken = false;
            var bills = await _context.Bills.Where(b => b.IsPaid == false && b.FkCustomerTables == id).ToListAsync();
            try
            {
                _context.Update(customerTables);
                await _context.SaveChangesAsync();
                foreach(var bill in bills)
                {
                    bill.IsPaid = true;
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            HttpContext.Session.SetString("message", "Stalas atrakintas sėkmingai!");
            return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
        }
    }
}

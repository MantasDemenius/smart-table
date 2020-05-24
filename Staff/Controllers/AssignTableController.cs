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

        public IActionResult BackToPrevious()
        {            
            return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTable(long id)
        { 
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
            try
            {
                foreach (var order in orders_to_assign)
                {
                    order.FkRegisteredUsers = HttpContext.Session.GetInt32("user_id");
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            HttpContext.Session.SetString("message", "Laisvi stalo užsakymai sėkmingai priskirti!");
            return Redirect("~/" + HttpContext.Session.GetString("previous_page"));
        }
    }
}

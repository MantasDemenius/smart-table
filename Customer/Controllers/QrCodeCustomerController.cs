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
            if (HttpContext.Session.GetInt32("customer_table_id") != null) {
                HttpContext.Session.SetInt32("user_role", 3);
                return Redirect("~/Menu");
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
                bill.FkCustomerTables = (long)id;

                _context.Add(bill);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("bill_id", Convert.ToInt32(bill.Id));

                // Send event to the waiter
                Events events = new Events();
                events.Type = 3;
                events.FkBills = bill.Id;
                _context.Add(events);
                await _context.SaveChangesAsync();
            }
            
            HttpContext.Session.SetInt32("user_role", 3);
            return Redirect("~/Menu");
        }

        private string generateJoinCode()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 10000).ToString();
        }
    }
}

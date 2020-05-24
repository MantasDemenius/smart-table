using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smart_table.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using smart_table.Models.DataBase;

namespace smart_table.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseContext _context;

        public HomeController(ILogger<HomeController> logger, DataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public IActionResult Index()
        {
            int user_role = -1;
            if (HttpContext.Session.GetInt32("user_role") != null)
                user_role = (int)HttpContext.Session.GetInt32("user_role");

            switch (user_role) {
                case 1:
                    return ConnectAsAdmin();
                case 2:
                    return ConnectAsWaiter();
                case 3:
                    return ConnectAsCustomer();
            }
            return View();
        }

        public IActionResult logout()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        
        public IActionResult ConnectAsAdmin()
        {
            HttpContext.Session.SetInt32("user_role", 1);
            HttpContext.Session.SetInt32("user_id", 1);
            return Redirect("~/ManageRegisteredUsers");
        }

        public IActionResult ConnectAsWaiter()
        {
            HttpContext.Session.SetInt32("user_role", 2);
            HttpContext.Session.SetInt32("user_id", 2);
            return Redirect("~/Notifications");
        }

        public IActionResult ConnectAsCustomer()
        {
            HttpContext.Session.SetInt32("user_role", 3);
            return Redirect("~/Menu");
        }

        // HACKS JUST FOR DEMONSTRATION
        public async Task<IActionResult> TakeTable(long? tableId) {
            if (tableId == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetInt32("customer_table_id") != null) {
                HttpContext.Session.SetInt32("user_role", 3);
                return Redirect("~/Menu");
            }

            if (HttpContext.Session.GetInt32("user_role") != (int)3) {
                HttpContext.Session.SetInt32("customer_table_id", Convert.ToInt32(tableId));
                string joinCode = generateJoinCode();
                HttpContext.Session.SetString("table_code", joinCode);

                var customerTable = await _context.CustomerTables.FindAsync(tableId);
                if (customerTable == null || customerTable.IsTaken) {
                    return View("~/Customer/Views/QrCodeCustomer/" + "QrCodeView.cshtml");
                }

                customerTable.JoinCode = joinCode;
                customerTable.IsTaken = true;
                _context.Entry(customerTable).State = EntityState.Modified;

                var bill = new Bills();
                bill.Evaluation = "";
                bill.FkCustomerTables = (long)tableId;

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
        // HACKS END

        private string generateJoinCode() {
            Random rnd = new Random();
            return rnd.Next(1000, 10000).ToString();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

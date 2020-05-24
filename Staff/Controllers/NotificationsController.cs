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
    public class NotificationsController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "Staff/Views/";

        public NotificationsController(DataBaseContext context)
        {
            _context = context;
        }

        
        [Route("Notifications")]
        public async Task<IActionResult> OpenNotificationsView()
        {
            
            ViewData["message"] = HttpContext.Session.GetString("message");
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            HttpContext.Session.SetString("message", "");
            HttpContext.Session.SetString("previous_page", "Notifications");

            var events = await _context.Events
                .Include(o => o.FkBillsNavigation)
                .Include(o => o.TypeNavigation)
                .Where(o => o.FkBillsNavigation.Orders.Count == 0 || o.FkBillsNavigation.Orders.Where(d => d.FkRegisteredUsers == null || d.FkRegisteredUsers == HttpContext.Session.GetInt32("user_id")).Count() > 0)
                .OrderByDescending(e => e.Id)
                .ToListAsync();

            foreach(var e in events)
            {
                var orders = await _context.Orders.Where(d => d.FkBills == e.FkBills).ToListAsync();
            }
            return View(_viewsPath + "ManageEventsView.cshtml", events);
        }
    }
}

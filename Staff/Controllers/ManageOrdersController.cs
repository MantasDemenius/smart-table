using System;
using System.Collections.Generic;
using System.Data;
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
    public class ManageOrdersController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "Staff/Views/";

        public ManageOrdersController(DataBaseContext context)
        {
            _context = context;
        }

        [Route("ManageOrders")]
        // GET: ManageOrders
        public async Task<IActionResult> OpenManageOrdersView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            var dataBaseContext = _context.Orders.Include(o => o.FkBillsNavigation).Include(o => o.FkCustomerTablesNavigation).Include(o => o.FkRegisteredUsersNavigation);
            return View(_viewsPath + "ManageOrdersView.cshtml", await dataBaseContext.ToListAsync());
        }

        // GET: ManageOrders/Details/5
        public async Task<IActionResult> OpenOrderView(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.FkBillsNavigation)
                .Include(o => o.FkCustomerTablesNavigation)
                .Include(o => o.FkRegisteredUsersNavigation)
                .Include(o => o.OrderDishes)
                .FirstOrDefaultAsync(m => m.Id == id);

            foreach (var orderDish in orders.OrderDishes)
            {
                var dish = await _context.Dishes
                    .FirstOrDefaultAsync(m => m.Id == orderDish.FkDishes);

            }

            if (orders == null)
            {
                return NotFound();
            }

            return View(_viewsPath + "OrderView.cshtml", orders);
        }

    }
}

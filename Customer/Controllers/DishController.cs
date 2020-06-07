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

namespace smart_table.Customer.Controllers
{
    public class DishController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "~/Customer/Views/Dish/";

        public DishController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Dish/Details/5
        public async Task<IActionResult> openDishView(long? id)
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes
                .Include(d => d.FkDishCategoriesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishes == null)
            {
                return NotFound();
            }

            return View(_viewsPath + "DishView.cshtml", dishes);
        }
    }
}

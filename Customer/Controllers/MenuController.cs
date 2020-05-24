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
    public class MenuController : Controller
    {
        private readonly DataBaseContext _context;
        private string _viewsPath = "~/Customer/Views/Menu/";

        public MenuController(DataBaseContext context)
        {
            _context = context;
        }


        // GET: Menu
        [Route("Menu")]
        public async Task<IActionResult> openMainMenuView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            var dataBaseContext = _context.MenuDishes.Include(m => m.FkDishesNavigation).Include(m => m.FkMenusNavigation);
            return View(_viewsPath + "MainMenuView.cshtml", await dataBaseContext.ToListAsync());
        }
    }
}

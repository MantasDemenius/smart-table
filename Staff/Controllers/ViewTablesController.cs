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
    }
}

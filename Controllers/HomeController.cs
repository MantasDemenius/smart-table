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

namespace smart_table.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
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

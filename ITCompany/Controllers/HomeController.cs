using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ITCompany.Models;

namespace ITCompany.Controllers
{
    public class HomeController : Controller
    {   
        private readonly ILogger<HomeController> _logger;
        public static CurrentUModel Cr;

       
        
        public HomeController(ILogger<HomeController> logger)
        {
            if (AccountController.Cr != null)
            Cr = AccountController.Cr;
            if (EventsController.Cr != null)
                Cr = EventsController.Cr;
            if (RolesController.Cr != null)
                Cr = RolesController.Cr;
            if (UsersController.Cr != null)
                Cr = UsersController.Cr;

            _logger = logger;
        }

        public IActionResult Index()
        {
            if(Cr!=null)
            ViewBag.name = Cr.name+"("+Cr.position+")";
            return View();
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPCoreSOFKids.Models;
using Microsoft.Extensions.Configuration;

namespace ASPCoreSOFKids.Controllers
{
    public class HomeController : Controller
    {
        public SOFKidsContext db_context;
        public IConfiguration Configuration { get; } 
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration; 
            db_context = new SOFKidsContext(Configuration);
        }
        public IActionResult Toys()
        {
            return View(db_context.Toys);
           
        }
       

        public IActionResult Cart()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Purchase()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult PurchaseHis()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

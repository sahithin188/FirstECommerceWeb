using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPSOFSports.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace ASPSOFSports.Controllers
{
    public class HomeController : Controller
    {
        public SOF586SportsContext db_context;
        public IConfiguration Configuration { get; }
        public string connectionString;
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("SOFSports");
            db_context = new SOF586SportsContext(Configuration);
        }
        public IActionResult Shopping()
        { 
             return View(db_context.Items);
            //return View();
        }

        public IActionResult Cart()
        {
            ViewData["Message"] = "Cart Items.";

            return View(db_context.PurchaseHistory.Include("Items").Where(o=> o.IsPurchased==false && o.UserInfoId==1) );
        }

        public IActionResult Payment()
        {
            ViewData["Message"] = "Payment Details.";

            return View();
        }

        public IActionResult PurchaseHis()
        {
            return View(db_context.PurchaseHistory.Include("Items").Where(o => o.IsPurchased == true && o.UserInfoId == 1));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        public IActionResult CreatItem()
        {
            ViewData["Message"] = "Create Item.";
            return View();
           
        }
        [HttpPost]
        public IActionResult AddItem(Items Item)
        {
             
            if (ModelState.IsValid)
            {
                db_context.Items.Add(Item); 
                db_context.SaveChanges();
                return RedirectToAction("Shopping");
            }
            else
                return View();
        }
        [HttpPost]
        public IActionResult Pay(PaymentAddress PayAddress)
        {
            PayAddress.UserInfoId = 1;
            if (ModelState.IsValid)
            {
                db_context.PaymentAddress.Add(PayAddress);
                foreach (PurchaseHistory ph in db_context.PurchaseHistory.Where(o => o.UserInfoId == 1 && o.IsPurchased == false))
                {
                    ph.IsPurchased = true;
                }
                db_context.SaveChanges(); 
                 return RedirectToAction("PurchaseHis"); 
            }
            else
                return View();
        }
        
        [HttpPost]
        public IActionResult AddtoCart(int ItemsId)
        {
           
            if (ModelState.IsValid)
            {
                if (db_context.PurchaseHistory.Where(o => o.ItemsId ==  ItemsId && o.UserInfoId == 1 && o.IsPurchased == false).Count() == 0)
                {
                    PurchaseHistory ph = new PurchaseHistory();
                    ph.ItemsId =  ItemsId ;
                    ph.UserInfoId = 1;
                    ph.Quantity = 1;
                    ph.IsPurchased = false;
                    db_context.PurchaseHistory.Add(ph);
                    db_context.SaveChanges();
                }
                else
                {
                    PurchaseHistory ph = db_context.PurchaseHistory.Where(o => o.ItemsId == ItemsId && o.UserInfoId == 1 && o.IsPurchased == false).FirstOrDefault() ;
                     ph.Quantity = ph.Quantity+1;
                    db_context.SaveChanges();
                }

                return RedirectToAction("Shopping");
                
            }
            else
                return View();
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int PurchaseHistoryId)
        {

            if (ModelState.IsValid)
            {
                if (db_context.PurchaseHistory.Where(o => o.PurchaseHistoryId == PurchaseHistoryId && o.UserInfoId == 1 && o.IsPurchased == false).Count() != 0)
                {
                    PurchaseHistory ph = db_context.PurchaseHistory.Where(o => o.PurchaseHistoryId == PurchaseHistoryId && o.UserInfoId == 1 && o.IsPurchased == false).FirstOrDefault();
                    db_context.PurchaseHistory.Remove(ph);
                    db_context.SaveChanges();
                }

                return RedirectToAction("Cart");

            }
            else
                return View();
        } 
        }

} 

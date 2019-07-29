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
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration; 
            db_context = new SOF586SportsContext(Configuration);
        }
        public IActionResult Shopping(string searching)
        {
            
            return View(db_context.Items.Where(o => o.Name.Contains(searching) || searching == null).ToList()); 

        }

        public IActionResult Cart()
        {
            ViewData["Message"] = "Cart Items.";

            return View(db_context.PurchaseHistory.Include("Items").Where(o=> o.IsPurchased==false && o.UserInfoId==1) );
        }

        public IActionResult Payment()
        {
            ViewData["Message"] = "Payment Details.";

            return View(db_context.PaymentAddress.Include("UserInfo").Where(o => o.UserInfoId  == 1));
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
        public IActionResult UpdateAddress(int PaymentAddressId)
        {
            ViewData["Message"] = "Update Address."; 
            return View(db_context.PaymentAddress.Include("UserInfo").Where(o => o.PaymentAddressId == PaymentAddressId).FirstOrDefault());

        }
        [HttpPost]
        public IActionResult DeleteAddress(int PaymentAddressId)
        {
            ViewData["Message"] = "Delete Address.";
            PaymentAddress pa = db_context.PaymentAddress.Where(o => o.PaymentAddressId == PaymentAddressId).FirstOrDefault();
            db_context.PaymentAddress.Remove(pa);
            db_context.SaveChanges();
            return RedirectToAction("Payment");

        }
        public IActionResult AddAddress()
        {
            ViewData["Message"] = "Add Address.";
            return View();

        }
        public IActionResult UpdatePrice(int ItemsId)
        {
            ViewData["Message"] = "Update Price.";
            return View(db_context.Items.Where(o => o.ItemsId  == ItemsId).FirstOrDefault());

        }
        [HttpPost]
        public IActionResult UpdateToyPrice(Items  item)
        {

            if (ModelState.IsValid)
            {
                Items  t = db_context.Items.Where(o => o.ItemsId  == item.ItemsId).FirstOrDefault();
                t.Price = item.Price;
                t.Description = item.Description;
                db_context.SaveChanges();
                return RedirectToAction("Shopping");
            }
            else
                return View("UpdatePrice");
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
            {if (db_context.PaymentAddress.Where(o => o.PaymentAddressId == PayAddress.PaymentAddressId).Count() > 0)
                {
                    PaymentAddress pa = db_context.PaymentAddress.Where(o => o.PaymentAddressId == PayAddress.PaymentAddressId).FirstOrDefault();
                    pa.AptNo = PayAddress.AptNo;
                    pa.Street = PayAddress.Street;
                    pa.State = PayAddress.State;
                    pa.City = PayAddress.City;
                    pa.ZipCode = PayAddress.ZipCode;
                    foreach (PurchaseHistory ph in db_context.PurchaseHistory.Where(o => o.UserInfoId == 1 && o.IsPurchased == false))
                    {
                        ph.IsPurchased = true;
                    }
                    db_context.SaveChanges();
                }
                else
                {
                    db_context.PaymentAddress.Add(PayAddress);
                    foreach (PurchaseHistory ph in db_context.PurchaseHistory.Where(o => o.UserInfoId == 1 && o.IsPurchased == false))
                    {
                        ph.IsPurchased = true;
                    }
                    db_context.SaveChanges();
                }
                 return RedirectToAction("PurchaseHis"); 
            }
            else
                return View();
        }
        [HttpPost]
        public IActionResult GenPDF(PurchaseHistory Phis)
        {
            PDFGenerator.ImportEntityList(db_context.PurchaseHistory.ToList()); 
            return RedirectToAction("PurchaseHis");
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

                return RedirectToAction("Cart");
                
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

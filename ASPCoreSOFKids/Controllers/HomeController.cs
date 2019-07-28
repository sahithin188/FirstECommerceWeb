using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPCoreSOFKids.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Toys(string searching)
        {
           // return View(db_context.Toys);
            return View(db_context.Toys.Where(o => o.ToyName.Contains(searching)|| searching == null ).ToList());

        }
       

        public IActionResult Cart()
        { 
            return View(db_context.CartToys.Include("Toy").Where(o =>o.CoustmerId  == 1));
        }

        public IActionResult Purchase()
        { 
            return View(db_context.MailingAddress.Include("Coustmer").Where(o => o.CoustmerId  == 1));
        }

        public IActionResult PurchaseHis()
        { 
            return View(db_context.PurchaseHistory.Include("Toy").Where(o =>o.CoustmerId == 1));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult CreatItem()
        {
            ViewData["Message"] = "Create Toy.";
            return View();

        }
        [HttpPost]
        public IActionResult UpdateAddress(int MailingAddressId)
        {
            ViewData["Message"] = "Update Address.";
            return View(db_context.MailingAddress.Include("Coustmer").Where(o => o.MailingAddressId == MailingAddressId).FirstOrDefault());

        }
        [HttpPost]
        public IActionResult DeleteAddress(int MailingAddressId)
        {
            ViewData["Message"] = "Delete Address.";
            MailingAddress  ma = db_context.MailingAddress.Where(o => o.MailingAddressId == MailingAddressId).FirstOrDefault();
            db_context.MailingAddress.Remove(ma);
            db_context.SaveChanges();
            return RedirectToAction("Purchase");

        }
        public IActionResult AddAddress()
        {
            ViewData["Message"] = "Add Address.";
            return View();

        }
        public IActionResult UpdatePrice(int ToyId)
        {
            ViewData["Message"] = "Update Price.";
            return View(db_context.Toys.Where(o=>o.ToyId==ToyId).FirstOrDefault());

        }

        [HttpPost]
        public IActionResult AddItem(Toys  Toy)
        {

            if (ModelState.IsValid)
            {
                db_context.Toys.Add(Toy);
                db_context.SaveChanges();
                return RedirectToAction("Toys");
            }
            else
                return View("CreateItem");
        }
         [HttpPost]
        public IActionResult UpdateToyPrice(Toys Toy)
        {

            if (ModelState.IsValid)
            {
                Toys t=db_context.Toys.Where(o=>o.ToyId==Toy.ToyId).FirstOrDefault();
                t.Price = Toy.Price;
                t.Description = Toy.Description;
                db_context.SaveChanges();
                return RedirectToAction("Toys");
            }
            else
                return View("UpdatePrice");
        }
        [HttpPost]
        public IActionResult Pay(MailingAddress  MailAddress)
        {
            MailAddress.CoustmerId  = 1;
            if (ModelState.IsValid)
            {
                if (db_context.MailingAddress .Where(o => o.MailingAddressId  == MailAddress.MailingAddressId).Count() > 0)
                {
                    MailingAddress ma = db_context.MailingAddress.Where(o => o.MailingAddressId == MailAddress.MailingAddressId).FirstOrDefault();
                    ma.AptNo = MailAddress.AptNo ;
                    ma.City = MailAddress.City;
                    ma.Street  = MailAddress.Street;                                   
                    ma.State = MailAddress.State ;
                    ma.ZipCode  = MailAddress.ZipCode ; 
                  
                }
                else
                {
                    db_context.MailingAddress.Add(MailAddress); 
                }
                foreach (CartToys ct in db_context.CartToys.Where(o => o.CoustmerId == 1))
                {
                    PurchaseHistory ph = new PurchaseHistory();
                    ph.ToyId = ct.ToyId;
                    ph.CoustmerId = ct.CoustmerId;
                    ph.Quantity = ct.Quantity;
                    db_context.PurchaseHistory.Add(ph);
                    db_context.CartToys.Remove(ct);
                }

                db_context.SaveChanges();
                return RedirectToAction("PurchaseHis");
            }
            else
                return View("Purchase");
        }

        [HttpPost]
        public IActionResult AddtoCart(int ToyId)
        {

            if (ModelState.IsValid)
            {
                if (db_context.CartToys.Where(o => o.ToyId == ToyId  && o.CoustmerId  == 1 ).Count() == 0)
                {
                    CartToys ct = new CartToys();
                    ct.ToyId = ToyId;
                    ct.CoustmerId = 1;
                    ct.Quantity = 1; 
                    db_context.CartToys.Add(ct);
                    db_context.SaveChanges();
                }
                else
                {
                    CartToys ct = db_context.CartToys.Where(o => o.ToyId == ToyId  && o.CoustmerId == 1 ).FirstOrDefault();
                    ct.Quantity = ct.Quantity + 1;
                    db_context.SaveChanges();
                }

                return RedirectToAction("Cart");

            }
            else
                return View("Toys");
        }
        [HttpPost]
        public IActionResult DeleteFromCart(int CartToyId)
        {

            if (ModelState.IsValid)
            {
                if (db_context.CartToys.Where(o => o.CartToyId  == CartToyId  && o.CoustmerId == 1).Count() != 0)
                {
                    CartToys ct = db_context.CartToys.Where(o => o.CartToyId  == CartToyId  && o.CoustmerId == 1).FirstOrDefault();
                    db_context.CartToys.Remove(ct);
                    db_context.SaveChanges();
                }

                return RedirectToAction("Cart");

            }
            else
                return View("Cart");
        }
    
}
}

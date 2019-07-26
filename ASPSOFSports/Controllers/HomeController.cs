﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPSOFSports.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

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
            //List<Items> itemlist = new List<Items>();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    //SqlDataReader
            //    connection.Open();

            //    string sql = "Select * From Items "; SqlCommand command = new SqlCommand(sql, connection);
            //    using (SqlDataReader dataReader = command.ExecuteReader())
            //    {
            //        while (dataReader.Read())
            //        {
            //            Items Item = new Items();
            //            Item.ItemsId = Convert.ToInt32(dataReader["ItemsId"]);
            //            Item.Name = Convert.ToString(dataReader["Name"]);
            //            Item.Price = Convert.ToDecimal(dataReader["Price"]);
            //            Item.Description = Convert.ToString(dataReader["Description"]);

            //            itemlist.Add(Item);
            //        }
            //    }
            //    connection.Close();
            //}

            //return View(itemlist);
             return View(db_context.Items);
            //return View();
        }

        public IActionResult Cart()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Payment()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult PurchaseHis()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Pay(Items Item)
        {
            if (ModelState.IsValid)
            {
                db_context.Items.Add(Item);
                db_context.SaveChanges();
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    string sql = $"Insert Into Items (ItemsId, Name, Price, PurchaseHistory) Values ('{Items.ItemsId}', '{ Items.Name}','{Items.Price}','{Items.PurchaseHistory}')";
                //    using (SqlCommand command = new SqlCommand(sql, connection))
                //    {
                //        command.CommandType = CommandType.Text;
                //        connection.Open();
                //        command.ExecuteNonQuery();
                //        connection.Close();
                //    }
                    return RedirectToAction("Payment");
                //}
            }
            else
                return View();
        }

        public IActionResult AddtoCart(Items Items)
        {
            PurchaseHistory ph = new PurchaseHistory();
            ph.ItemsId = Items.ItemsId;
            ph.UserInfoId = 1;
            ph.Quantity = 1;
            ph.IsPurchased = false;
            if (ModelState.IsValid)
            {
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    string sql = $"Insert Into PurchaseHistory (PurchaseHistoryId, ItemsId, UserInfoId, Quantity,IsPurchased) Values ('{Items.ItemsId}', '{ Items.Name}','{Items.Price}','{Items.PurchaseHistory}')";
                //    using (SqlCommand command = new SqlCommand(sql, connection))
                //    {
                //        command.CommandType = CommandType.Text;
                //        connection.Open();
                //        command.ExecuteNonQuery();
                //        connection.Close();
                //    }
                    return RedirectToAction("Payment");
                //}
            }
            else
                return View();
        }
    }

} 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day06.Models;
using Microsoft.Extensions.Configuration;

namespace Day06.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Widgets");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public IActionResult Widgets()
        {
            var results = new List<WidgetViewModel>();
            var categoryId = int.Parse(Request.Query["categoryId"].ToString());
            
            
            
            
            
            
            
            
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Widgets WHERE CategoryId=@CAT_ID", connection);
                command.Parameters.Add("@CAT_ID", SqlDbType.Int).Value = categoryId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new  WidgetViewModel { Name = reader["Name"].ToString(), CategoryId = int.Parse(reader["CategoryId"].ToString())});
                        
                    }
                }
            }
            return View(results);
        }
        
        public IActionResult WidgetsWorse()
        {
            var results = new List<WidgetViewModel>();
            var categoryId = Int64.Parse(Request.Query["categoryId"].ToString());









            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Widgets WHERE CategoryId=@CAT_ID", connection);
                command.Parameters.Add("@CAT_ID", SqlDbType.Int).Value = categoryId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new  WidgetViewModel { Name = reader["Name"].ToString(), CategoryId = int.Parse(reader["CategoryId"].ToString())});
                        
                    }
                }
            }
            return View(results);
        }
        
        public IActionResult WidgetsWorst()
        {
            var results = new List<WidgetViewModel>();
            var categoryId = Int64.Parse(Request.Query["categoryId"].ToString());









            using (var connection = new SqlConnection("Server=tcp:mgtrainingsqlserver.database.windows.net,1433;Initial Catalog=owaspdbsafe;Persist Security Info=False;User ID=academymg;Password=M3gs0ft2019;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Widgets WHERE CategoryId=@CAT_ID", connection);
                command.Parameters.Add("@CAT_ID", SqlDbType.Int).Value = categoryId;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new  WidgetViewModel { Name = reader["Name"].ToString(), CategoryId = int.Parse(reader["CategoryId"].ToString())});
                        
                    }
                }
            }
            return View(results);
        }
    }
}
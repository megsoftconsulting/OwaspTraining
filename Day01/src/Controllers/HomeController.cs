using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day01.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Day01.Controllers
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
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string user, string password)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM Users WHERE Username='{user}' AND Password='{password}'", connection);
                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        ViewData.Add(new KeyValuePair<string, object>("Message","Login Successful"));
                    }
                    else
                    {
                        ViewData.Add(new KeyValuePair<string, object>("Message", "Login Unsuccessful"));
                    }
                }
            }
            return View();
        }

        public IActionResult LoginSecure()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult LoginSecure(string user, string password)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                ViewData.Add(new KeyValuePair<string, object>("Message", "Login Unsuccessful"));
            }
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT * FROM Users WHERE Username=@username AND Password=@password", connection);
                command.Parameters.AddWithValue("@username", user);
                command.Parameters.AddWithValue("@password", password);
                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        ViewData.Add(new KeyValuePair<string, object>("Message","Login Successful"));
                    }
                    else
                    {
                        ViewData.Add(new KeyValuePair<string, object>("Message", "Login Unsuccessful"));
                    }
                }
            }
            return View();
        }

        public IActionResult Products()
        {
            var results = new List<OrdersViewModel>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Name, Type FROM Orders", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new  OrdersViewModel{ Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(results);
        }

        public IActionResult GetProducts([FromQuery] string name)
        {
            var results = new List<object>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand($"SELECT Name, Type FROM Orders WHERE Name LIKE '%{name.ToUpper()}%'", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new { Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Json(results);
        }
        
        public IActionResult ProductsSecure()
        {
            var results = new List<OrdersViewModel>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Name, Type FROM Orders", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new  OrdersViewModel{ Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(results);
        }
        
        public IActionResult GetProductsSecure([FromQuery] string name)
        {
            var results = new List<object>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Name, Type FROM Orders WHERE Name LIKE '%'+@name+'%'", connection);
                    command.Parameters.Add(new SqlParameter("@name", name.ToUpper()));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new { Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Json(results);
        }
        
        public IActionResult Doomsday()
        {
            var results = new List<OrdersViewModel>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Name, Type FROM Orders", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new  OrdersViewModel{ Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(results);
        }

        public IActionResult DoomsdaySecure()
        {
            var results = new List<OrdersViewModel>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("SafeConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Name, Type FROM Orders", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new  OrdersViewModel{ Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View(results);
        }
        
        public IActionResult GetProductsRead([FromQuery] string name)
        {
            var results = new List<object>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("SafeConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand($"SELECT Name, Type FROM Orders WHERE Name LIKE '%{name.ToUpper()}%'", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new { Name = reader["Name"].ToString(), Type = reader["Type"].ToString()});
                        
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return Json(results);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day02.Models;
using Microsoft.Extensions.Configuration;

namespace Day02.Controllers
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Passwords()
        {
            var results = new List<User>();
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM UsersPasswordVulnerable", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new  User{ Username = reader["Username"].ToString(), Password = reader["Password"].ToString(), Type = reader["Type"].ToString()});
                        
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

        [HttpGet]
        public IActionResult QueryStrings([FromQuery] string user = "", [FromQuery] string password = "")
        {
            if (!string.IsNullOrEmpty(user))
            {
                ViewData.Add(new KeyValuePair<string, object>("Message", $"Awesome but look, Your Query String contains sensitive data: \"{Request.QueryString}\"" ));
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult Products([FromQuery] int? ownerId)
        {
            var results = new List<Product>();
            if (ownerId != null)
            {
                var sqlString = "SELECT Id, Name, Owner FROM Products WHERE OwnerId=@id";
                try
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var command = new SqlCommand(sqlString, connection);
                        command.Parameters.Add(new SqlParameter("@id", ownerId));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Add(new  Product{ Id = reader.GetInt32(0), Name = reader.GetString(1), Owner = reader.GetString(2)});
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return View(results);
        }
    }
}

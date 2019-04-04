using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Day04.Models;

namespace Day04.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GoToAdmin(bool fromAdminError = false)
        {
            Response.Cookies.Append("IsAdmin", "0");
            if(fromAdminError) ViewData["ErrorMessage"] = "The user is not admin";
            return View();
        }

        public IActionResult Admin()
        {
            if (Request.Cookies.ContainsKey("IsAdmin"))
            {
                var cookieValue = Request.Cookies["IsAdmin"];
                if (cookieValue == "1") return View();
                return RedirectToAction("GoToAdmin", new { fromAdminError = true});

            }
            else
            {
                return RedirectToAction("GoToAdmin");
            }
        }

        public IActionResult Hijacking()
        {
            return View(); 
        }

        public IActionResult Xss(string comment = "")
        {
            ViewData["Comment"] = comment;
            return View();
        }

        [HttpPost]
        public IActionResult SendData([FromBody] string cookie = "")
        {

            return Json(new { Data = "", Status = "Lo logre"});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
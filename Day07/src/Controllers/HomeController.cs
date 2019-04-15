using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Day07.Models;
using Microsoft.Extensions.Configuration;
using Megsoft.Owasp;

namespace Day07.Controllers
{
    public class HomeController : OwaspBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowNuget()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public HomeController(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
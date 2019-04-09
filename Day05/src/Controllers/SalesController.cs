using System.Linq;
using System.Security.Claims;
using Day05.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day05.Controllers
{
    [Authorize(Roles = "Administrador,Ventas")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CustomerSales(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Home");
            var user = _context.Users.Where(x => x.Id == id).Include(x => x.Sales).FirstOrDefault();
            if (user == null) return RedirectToAction("Index", "Home");
            return View(user);
        }

        public IActionResult CustomerSalesFixed()
        {
            if(User == null) return RedirectToAction("Index", "Home");
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.Id == id).Include(x => x.Sales).FirstOrDefault();
            return View(user);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Day05.Data;
using Day05.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day05.Controllers
{
    [Authorize(Roles="Administrador")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Roles()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }
        
        [HttpGet]
        public IActionResult Users()
        {
            var users = _context.Users.OrderBy(x => x.Email).ToList();
            return View(users);
        }
        
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole([ModelBinder] ApplicationRole role)
        {
            try
            {
                role.NormalizedName = role.Name.ToUpper().Trim();
                _context.Roles.Add(role);
                _context.SaveChanges();
                return RedirectToAction("Roles");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AssignRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Users");
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            var roles = _context.Roles.ToList();
            var userRoles = _context.UserRoles.Where(x => x.UserId == id).ToList();
            var roleList = new List<RoleViewModel>();
            foreach (var item in roles)
            {
                var userRole = userRoles.FirstOrDefault(x => x.RoleId == item.Id);
                roleList.Add(new RoleViewModel { RoleId = item.Id, Name = item.Name, Selected = userRole != null});
            }    
            var vm = new UserRoleViewModel();
            vm.UserId = user.Id;
            vm.Email = user.Email;
            vm.RoleViewModels = roleList;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AssignRole(UserRoleViewModel vm)
        {
            try
            {
                var roles = _context.UserRoles.Where(x => x.UserId == vm.UserId);
                _context.UserRoles.RemoveRange(roles);
                _context.SaveChanges();
                foreach (var item in vm.RoleViewModels)
                {
                    if(!item.Selected) continue;
                    _context.UserRoles.Add(new IdentityUserRole<string>() {RoleId = item.RoleId, UserId = vm.UserId});
                }

                _context.SaveChanges();
                return RedirectToAction("Users");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ViewData["Error"] = e.Message;
            }
            return View(vm);
        }

        // Want to create Sales for each user? Use this method and will generate sales for each user in your DB
        // Your friend: @codercampos
        private void SetRandomSales()
        {
            var users = _context.Users.Include(x => x.Sales);
            foreach (var user in users)
            {
                user.Sales.Add(new Sale
                {
                    CustomerName = "A CUSTOMER for " + user.UserName, Amount = RandomNumber(50000, 500000), OwnerId = user.Id,
                    CreatedAt = DateTime.Now, InvoiceCode = user.Email.Substring(0, 3) + "0" + RandomNumber(10, 70) 
                });
            }

            _context.SaveChanges();
        }
        
        public int RandomNumber(int min, int max)  
        {  
            Random random = new Random();  
            return random.Next(min, max);  
        }  
    }
}
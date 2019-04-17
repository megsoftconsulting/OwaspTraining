using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Day08.Data;
using Microsoft.AspNetCore.Mvc;
using Day08.Models;

namespace Day08.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction("RedirectSample");
        }

        public IActionResult RedirectSample()
        {
            return View();
        }

        public IActionResult RedirectWhitelist()
        {
            return View();
        }

        public IActionResult RedirectAnother()
        {
            return View();
        }

        public IActionResult RedirectUrl(string site)
        {
            if (string.IsNullOrEmpty(site)) return RedirectToAction("Index");
            // TODO Log here
            ViewData.Add("url", site);
            return View();
        }
        
        public IActionResult RedirectWithWhiteList(string site)
        {
            if (string.IsNullOrEmpty(site)) return RedirectToAction("Index");
            
            // Check if the URL is on the whitelist, otherwise, return Error action
            if (!_context.TrustedUrls.Any(x => x.Name == site)) return RedirectToAction("Error");
            
            // TODO Log here
            ViewData.Add("url", site);
            return View();
        }
        
        public IActionResult RedirectWithMoreMitigation(string site)
        {
            try
            {
                if (string.IsNullOrEmpty(site)) return RedirectToAction("Index");
            
                // Check if the Referer is in the header, otherwise, return Error action
                var referrer = Request.Headers["Referer"].ToString();
                if (string.IsNullOrEmpty(referrer)) return RedirectToAction("Error", new { message = referrer });
            
                // Check if the Referer URL is coming from the out Host, otherwise, return Error action
                var referrerUrl = new Uri(referrer);
                if (referrerUrl.Host != Request.Host.Host)
                    return RedirectToAction("Error", new { message = $"Referer = {referrerUrl.ToString()}; Host: {Request.Host.ToString()}; Referer Host: {referrerUrl.Host}"});
            
                // Check if the URL is on the whitelist, otherwise, return Error action
                if (!_context.TrustedUrls.Any(x => x.Name == site)) return RedirectToAction("Error");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Error", new { message = e.Message});
            }
            
            // TODO Log here
            ViewData.Add("url", site);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message = "")
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message});
        }
    }
}
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Day03.Controllers
{
    public class HomeController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;
        private UrlEncoder _urlEncoder;
        public HomeController(HtmlEncoder htmlEncoder, JavaScriptEncoder javaScriptEncoder, UrlEncoder urlEncoder)
        {
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javaScriptEncoder;
            _urlEncoder = urlEncoder;
        }
        public IActionResult Index()
        {
            return RedirectToAction("XssSample");
        }

        public IActionResult XssSample(string comment = "")
        {
            ViewBag.Comment = comment;
            return View();
        }

        public IActionResult XssAdvanced(string comment = "")
        {
            ViewBag.Comment = comment;
            return View();
        }
        
        public IActionResult XssRedirect(string comment = "")
        {
            ViewBag.Comment = comment;
            return View();
        }
        
        public IActionResult XssDDOSAttack(string comment = "")
        {
            ViewBag.Comment = comment;
            return View();
        }
        
        public IActionResult XssFixed(string comment = "")
        {
            ViewBag.Comment = _htmlEncoder.Encode(comment);
            return View();
        }
    }
}
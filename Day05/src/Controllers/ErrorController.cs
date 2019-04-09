using Microsoft.AspNetCore.Mvc;

namespace Day05.Controllers
{
    public class ErrorController : Controller
    {
        [Route("401")]
        public IActionResult Error401()
        {
            return View();
        }
    }
}
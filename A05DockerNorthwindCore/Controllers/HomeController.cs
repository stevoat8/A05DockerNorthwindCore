using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A05DockerNorthwindCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A05DockerNorthwindCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly NorthwindContext ctx;

        public HomeController(NorthwindContext northwindContext)
        {
            ctx = northwindContext;
        }

        public IActionResult Index()
        {
            ViewData["country"] = new SelectList(ctx.Customers.Select(cust => cust.Country).Distinct());
            return View();
        }

        public IActionResult FilterCustomers(string country)
        {
            //TODO: hier geht's weiter. Loading-strategie anpassen
            var customers = ctx.Customers.Where(cust => cust.Country == country);
            return PartialView("_customers", customers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

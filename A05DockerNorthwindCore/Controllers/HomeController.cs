using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A05DockerNorthwindCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            ViewData["countries"] = new SelectList(ctx.Customers.Select(cust => cust.Country).Distinct());
            return View();
        }

        public IActionResult GetCustomers(string country)
        {
            var customers = ctx.Customers
                .Where(cust => cust.Country == country)
                .ToList();
            return PartialView("_customers", customers);
        }

        public IActionResult GetOrders(string customerId)
        {
            List<string> orders = ctx.Orders
                .Where(o => o.CustomerId == customerId)
                .OrderBy(o => o.OrderDate)
                .Select(o => o.OrderId.ToString())
                .ToList();
            return PartialView("_orders", orders);
        }

        public IActionResult GetDetailedOrder(int orderId)
        {
            Orders detailedOrder = ctx.Orders
                .Where(o => o.OrderId == orderId)
                .Include(o => o.OrderDetails)
                .Include(o => o.Employee)
                .FirstOrDefault();

            return PartialView("_orderDetails", detailedOrder);
        }

        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}

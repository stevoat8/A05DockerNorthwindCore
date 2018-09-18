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
            ViewData["country"] = new SelectList(ctx.Customers.Select(cust => cust.Country).Distinct());
            return View();
        }

        public IActionResult FilterCustomers(string country)
        {
            var customers = ctx.Customers
                .Where(cust => cust.Country == country)
                .ToList();
            return PartialView("_customers", customers);
        }

        public IActionResult GetOrders(string customerId)
        {
            var orders = ctx.Orders
                .Where(o => o.CustomerId == customerId)
                .OrderBy(o => o.OrderDate)
                .Select(o => o.OrderId)
                .ToList();
            ViewData["orderId"] = new SelectList(orders);
            return PartialView("_orders");
        }

        public IActionResult GetOrderDetails(int orderId)
        {
            Orders order = ctx.Orders
                .Where(o => o.OrderId == orderId)
                .Include(o => o.OrderDetails)
                .Include(o => o.Employee)
                .FirstOrDefault();

            //string bestellt = order.OrderDate.HasValue 
            //    ? order.OrderDate.Value.ToShortDateString() 
            //    : "-";
            //string shipped = order.ShippedDate.HasValue
            //    ? order.ShippedDate.Value.ToShortDateString()
            //    : "-";

            //OrderDetails details = order.OrderDetails.First();
            //string totalPrice = (details.UnitPrice * details.Quantity * (decimal)details.Discount).ToString("C2");

            //string employee = order.Employee.FirstName + " " + order.Employee.LastName;

            return PartialView("_orderDetails", order);
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

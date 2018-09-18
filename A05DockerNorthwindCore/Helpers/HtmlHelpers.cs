using A05DockerNorthwindCore.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A05DockerNorthwindCore.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent DisplayOrder(this IHtmlHelper helper, Orders order)
        {
            string header =
                "<thead><tr>" +
                "<th>BestellNr</th>" +
                "<th>Bestellt</th>" +
                "<th>Ausgeliefert</th>" +
                "<th>Gesamtwert</th>" +
                "<th>Bearbeitet durch</th>" +
                "</tr></thead>";

            string ordered = order.OrderDate.HasValue ? order.OrderDate.Value.ToShortDateString() : "-";
            string shipped = order.ShippedDate.HasValue ? order.ShippedDate.Value.ToShortDateString() : "-";
            string employee = order.Employee.FirstName + " " + order.Employee.LastName;

            decimal sum = 0;
            foreach (OrderDetails detail in order.OrderDetails)
            {
                decimal price = detail.UnitPrice * detail.Quantity;
                price += price * (decimal)detail.Discount;
                sum += price;
            }
            string totalPrice = sum.ToString("C2");

            string body =
                "<tbody><tr>" +
                $"<td>{order.OrderId}</td>" +
                $"<td>{ordered}</td>" +
                $"<td>{shipped}</td>" +
                $"<td>{totalPrice}</td>" +
                $"<td>{employee}</td>" +
                "</tr></tbody>";

            return new HtmlString(
                $"<table class=\"table\">{header}{body}</table>");
        }

        public static IHtmlContent DisplayOrder2(this IHtmlHelper helper, Orders order)
        {


            string ordered = order.OrderDate.HasValue ? order.OrderDate.Value.ToShortDateString() : "-";
            string shipped = order.ShippedDate.HasValue ? order.ShippedDate.Value.ToShortDateString() : "-";
            string employee = order.Employee.FirstName + " " + order.Employee.LastName;

            decimal totalPrice = 0;
            foreach (OrderDetails detail in order.OrderDetails)
            {
                decimal discount = 1.0m + (decimal)detail.Discount;
                totalPrice += detail.UnitPrice * discount * detail.Quantity;
            }
            string totalPriceStr = totalPrice.ToString("C2");

            string table =
                 "<table class=\"table\">" +
                     "<thead>" +
                         "<tr>" +
                             "<th>BestellNr</th>" +
                            $"<th>{order.OrderId}</th>" +
                         "</tr>" +
                     "</thead>" +
                     "<tbody>" +
                         "<tr>" +
                             "<td>Bestellt</td>" +
                            $"<td>{ordered}</td>" +
                         "</tr>" +
                         "<tr>" +
                             "<td>Ausgeliefert</td>" +
                            $"<td>{shipped}</td>" +
                         "</tr>" +
                         "<tr>" +
                             "<td>Gesamtwert</td>" +
                            $"<td>{totalPriceStr}</td>" +
                         "</tr>" +
                         "<tr>" +
                             "<td>Bearbeitet durch</td>" +
                            $"<td>{employee}</td>" +
                         "</tr>" +
                     "</tbody>" +
                 "</table>";

            return new HtmlString(table);
        }
    }
}

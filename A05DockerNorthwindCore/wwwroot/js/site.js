/// <reference path="../lib/jquery/dist/jquery.js" />
$(document).ready(function () {

    $("#countries").change(function () {
        var country = $("#countries").val();
        $("#orderDetails").empty();
        $("#orderList").empty();

        $("#customerList").load("/Home/GetCustomers", { "country": country }, function () {
            $("input:radio[name=customerRbg]").change(function () {
                $("#orderDetails").empty();
                var customerId = $("input:radio[name=customerRbg]:checked").val();

                $("#orderList").load("/Home/GetOrders", { "customerId": customerId }, function () {
                    $("#orderIds").change(function () {
                        var orderId = $("#orderIds").val();
                        $("#orderDetails").load("/Home/GetDetailedOrder", { "orderId": orderId });
                    });
                });
            });
        });
    });
});
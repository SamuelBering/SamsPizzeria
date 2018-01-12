using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SamsPizzeria.Services;

namespace SamsPizzeria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ViewResult> OrderList()
        {
            return View(await _orderService.GetOrders());
        }

        public IActionResult UpdateOrderStatus(int orderId, bool status)
        {
            _orderService.UpdateOrderStatus(orderId, status);

            return Json(new { Status = status });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderStatus(int orderId, bool status)
        {
            _orderService.UpdateOrderStatus(orderId, status);

            return Json(new { Status = status });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                _orderService.DeleteOrder(orderId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(new
            {
                Message = "Order with id: " + orderId + "was deleted",
                OrderId = orderId
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var orderVM = await _orderService.GetOrderDetails(id);

            return PartialView("_Details", orderVM);
        }

    }
}
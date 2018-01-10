using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Services;

namespace SamsPizzeria.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminOrderController : Controller
    {
        private IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ViewResult OrderList()
        {            
            return View(_orderService.Orders);
        }
    }
}
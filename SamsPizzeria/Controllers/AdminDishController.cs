using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Models.ViewModels;
using SamsPizzeria.Services;

namespace SamsPizzeria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDishController : Controller
    {
        private IDishService _dishService;

        public AdminDishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        public ViewResult Index()
        {
            return View(_dishService.GetDishes());
        }

        public ViewResult EditDish(int id)
        {
            return View(_dishService.GetDish(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult EditDish(DishModificationModel dish)
        {
            return View();
        }


    }
}
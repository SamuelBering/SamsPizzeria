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

        public ViewResult EditDish(int? id)
        {
            DishModificationModel dish = id != null ? _dishService.GetDish(id.Value) : _dishService.GetEmptyDish();

            return View(dish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDish(DishModificationModel dish)
        {
            if (ModelState.IsValid)
            {
                _dishService.AddOrUpdate(dish);
                return RedirectToAction(nameof(Index));
            }

            return View(_dishService.AddCategoriesAndProductsSelectList(dish));
        }

        public ViewResult ViewIngredients()
        {
            return View(_dishService.GetIngredients());
        }

        public ViewResult EditIngredient(int? id)
        {
            return View(id != null ? _dishService.GetIngredient(id.Value) : null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditIngredient(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _dishService.AddOrUpdateIngredient(ingredient);
                return RedirectToAction(nameof(ViewIngredients));
            }

            return View(ingredient);
        }

    }
}
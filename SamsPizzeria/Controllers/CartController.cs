﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Infrastructure;
using SamsPizzeria.Models;
using SamsPizzeria.Models.ViewModels;
using SamsPizzeria.Services;

namespace SamsPizzeria.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart cart;
        private IDiscountService discountService;

        public CartController(IProductRepository repo, Cart cartService, IDiscountService discountService)
        {
            repository = repo;
            cart = cartService;
            this.discountService = discountService;

        }

        public async Task<ViewResult> Index(string returnUrl)
        {
            var discounts = await this.discountService.GetDiscountsAsync(cart);

            ViewBag.DiscountsTotalValue = discounts?.Sum(d => d.Value) ?? 0;

            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl,
                Discounts = discounts
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            Matratt dish = repository.Dishes
            .FirstOrDefault(d => d.MatrattId == id);
            if (dish != null)
            {
                cart.AddItem(dish, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            Matratt dish = repository.Dishes
            .FirstOrDefault(d => d.MatrattId == id);
            if (dish != null)
            {
                cart.RemoveLine(dish);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

    }
}
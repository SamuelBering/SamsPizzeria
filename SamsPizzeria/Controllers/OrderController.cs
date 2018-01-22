using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Models;
using SamsPizzeria.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SamsPizzeria.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        private IDiscountService discountService;

        public OrderController(IOrderRepository repoService, Cart cartService, IDiscountService discountService)
        {
            repository = repoService;
            cart = cartService;
            this.discountService = discountService;
        }

        public async Task<RedirectToActionResult> SendOrder()
        {
            var discounts = await this.discountService.GetDiscountsAsync(cart);

            var discountsTotalValue = discounts?.Sum(d => d.Value) ?? 0;

            Bestallning order = new Bestallning
            {
                BestallningDatum = DateTime.Now,
                Totalbelopp = (int)((cart.ComputeTotalValue() - discountsTotalValue) + 0.5M),
                Levererad = false,
                UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            order.BestallningMatratt = new List<BestallningMatratt>();

            foreach (CartLine line in cart.Lines)
            {
                order.BestallningMatratt.Add( 
                    new BestallningMatratt
                    {   
                        Matratt = line.Dish,
                        Antal = line.Quantity
                    }
                    );
            }

             repository.SaveOrder(order);

            await discountService.SaveBonus(discounts, cart);

            return RedirectToAction(nameof(Completed));
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

    }
}
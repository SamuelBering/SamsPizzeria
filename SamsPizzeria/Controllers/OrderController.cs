using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SamsPizzeria.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
        }

        public RedirectToActionResult SendOrder()
        {

            Bestallning order = new Bestallning
            {
                BestallningDatum = DateTime.Now,
                Totalbelopp = (int)(cart.ComputeTotalValue() + 0.5M),
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

            return RedirectToAction(nameof(Completed));
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

    }
}
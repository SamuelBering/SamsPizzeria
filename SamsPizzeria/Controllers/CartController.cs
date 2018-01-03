using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Infrastructure;
using SamsPizzeria.Models;
using SamsPizzeria.Models.ViewModels;

namespace SamsPizzeria.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            Matratt dish = repository.Dishes
            .FirstOrDefault(d => d.MatrattId == id);
            if (dish != null)
            {
                Cart cart = GetCart();
                cart.AddItem(dish, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            Matratt dish = repository.Dishes
            .FirstOrDefault(d => d.MatrattId == id);
            if (dish != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(dish);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
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
        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
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
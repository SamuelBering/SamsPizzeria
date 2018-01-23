using Microsoft.AspNetCore.Mvc;
using SamsPizzeria.Infrastructure;
using SamsPizzeria.Models;
using SamsPizzeria.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        private IDiscountService discountService;

        public CartSummaryViewComponent(Cart cartService,IDiscountService discountService)
        {
            cart = cartService;
            this.discountService = discountService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string returnUrl)
        {
            if (returnUrl == null)
                ViewBag.ReturnUrl = HttpContext.Request.PathAndQuery();
            else
                ViewBag.ReturnUrl = returnUrl;

            var discounts = await this.discountService.GetDiscountsAsync(cart);

            ViewBag.DiscountsTotalValue = discounts?.Sum(d => d.Value) ?? 0;

            return View(cart);
        }
    }
}

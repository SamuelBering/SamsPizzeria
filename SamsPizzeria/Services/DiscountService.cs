using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SamsPizzeria.Models;

namespace SamsPizzeria.Services
{
    public class DiscountService : IDiscountService
    {
        private IHttpContextAccessor httpContextAccessor;
        private UserManager<AppUser> userManager;

        public DiscountService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<ICollection<Discount>> GetDiscountsAsync(Cart cart)
        {
            var user = httpContextAccessor.HttpContext.User;

            if ((user?.IsInRole("PremiumUser") ?? false) && cart.Lines.Count>0)
            {
                AppUser currentUser = await userManager.GetUserAsync(user);
                List<Discount> discounts = new List<Discount>();

                int totalDishes = cart.Lines.Sum(l => l.Quantity);
                if (totalDishes >= 3)
                {
                    discounts.Add(new Discount
                    {
                        Description = "20% rabatt",
                        Value = cart.Lines.Sum(l => l.Dish.Pris * l.Quantity) * 0.2M
                    });
                }

                int currentBonus = currentUser.Bonus;
                if (currentBonus >= 100)
                {
                    var bonusValue = cart.Lines.Min(l => l.Dish.Pris);
                    discounts.Add(new Discount
                    {
                        Description = "Bonuscheck",
                        Value = bonusValue,
                        UsedBonus = 110
                    });
                }
                return discounts;
            }
            else
                return null;
        }

        public async Task<int> SaveBonus(ICollection<Discount> discounts, Cart cart)
        {
            var user = httpContextAccessor.HttpContext.User;

            if (user?.IsInRole("PremiumUser") ?? false)
            {
                AppUser currentUser = await userManager.GetUserAsync(user);

                int currentBonus = currentUser.Bonus;

                if (discounts != null)
                    foreach (Discount discount in discounts)
                        currentBonus -= discount.UsedBonus;

                int totalDishes = cart.Lines.Sum(l => l.Quantity);
                currentBonus += totalDishes * 10;

                currentUser.Bonus = currentBonus;

                var result = await userManager.UpdateAsync(currentUser);
                if (result.Succeeded)
                    return currentUser.Bonus;
            }

            return -1;
        }
    }
}

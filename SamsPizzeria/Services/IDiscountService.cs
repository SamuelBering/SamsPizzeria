using SamsPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Services
{
    public interface IDiscountService
    {
        Task<ICollection<Discount>> GetDiscountsAsync(Cart cart);
        Task<int> SaveBonus(ICollection<Discount> discounts, Cart cart);
    }
}

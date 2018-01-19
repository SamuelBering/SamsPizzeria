using SamsPizzeria.Models;
using SamsPizzeria.Services;
using System.Collections.Generic;

namespace SamsPizzeria.Models.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public string ReturnUrl { get; set; }
    }
}
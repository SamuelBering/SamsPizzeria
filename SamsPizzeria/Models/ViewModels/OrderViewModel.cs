using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Delivered { get; set; }

        public List<SelectListItem> OrderStatusSelectList;
    }
}

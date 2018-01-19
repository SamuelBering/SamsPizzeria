using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models.ViewModels
{
    public class OrderDetailsModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<DishDetailsModel> Dishes { get; set; }
        public int Discount { get; set; }
        public int TotalAmount { get; set; }
    }
}

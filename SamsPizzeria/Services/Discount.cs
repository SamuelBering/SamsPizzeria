using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Services
{
    public class Discount
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int UsedBonus { get; set; }
    }
}

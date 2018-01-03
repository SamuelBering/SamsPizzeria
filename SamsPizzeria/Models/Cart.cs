using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Matratt dish, int quantity)
        {
            CartLine line = lineCollection
            .Where(cl => cl.Dish.MatrattId == dish.MatrattId)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Matratt dish) =>
                lineCollection.RemoveAll(l => l.Dish.MatrattId == dish.MatrattId);

        public virtual decimal ComputeTotalValue() =>
                lineCollection.Sum(cl => cl.Dish.Pris * cl.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Matratt Dish { get; set; }
        public int Quantity { get; set; }
    }
}

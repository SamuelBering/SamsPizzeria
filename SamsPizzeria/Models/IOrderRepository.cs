using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public interface IOrderRepository
    {
        IQueryable<Bestallning> Orders { get; }
        void UpdateOrderStatus(int orderId, bool status);
        void SaveOrder(Bestallning order);
    }
}

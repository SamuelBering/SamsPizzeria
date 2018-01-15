using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private TomasosContext _context;

        public EFOrderRepository(TomasosContext context)
        {
            _context = context;
        }

        public IQueryable<Bestallning> Orders => _context.Bestallning
                    .Include(b => b.BestallningMatratt)
                        .ThenInclude(bm => bm.Matratt);

        public void UpdateOrderStatus(int orderId, bool status)
        {
            var order = _context.Bestallning.Single(o => o.BestallningId == orderId);
            order.Levererad = status;
            _context.SaveChanges();
        }

        public void SaveOrder(Bestallning order)
        {
            _context.AttachRange(order.BestallningMatratt.Select(bm => bm.Matratt));
            if (order.BestallningId == 0)
            {
                _context.Bestallning.Add(order);
            }
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = Orders.Single(o =>
                o.BestallningId == orderId);

            order.BestallningMatratt.Clear();
            _context.SaveChanges();

            _context.Bestallning.Remove(order);
            _context.SaveChanges();
        }


    }
}

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
                    .Include(b => b.Kund)
                    .Include(b => b.BestallningMatratt)
                        .ThenInclude(bm => bm.Matratt);

        public void SaveOrder(Bestallning order)
        {
            _context.AttachRange(order.BestallningMatratt.Select(bm => bm.Matratt));
            if (order.BestallningId == 0)
            {
                _context.Bestallning.Add(order);
            }
            _context.SaveChanges();
        }

    }
}

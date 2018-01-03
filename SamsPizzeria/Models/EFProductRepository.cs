using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public class EFProductRepository:IProductRepository
    {
        TomasosContext _context;

        public EFProductRepository(TomasosContext context)
        {
            _context = context;
        }

        public IQueryable<Matratt> Dishes => _context.Matratt;
        public IQueryable<Produkt> Products => _context.Produkt;

    }
}

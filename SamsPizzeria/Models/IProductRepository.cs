using SamsPizzeria.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public interface IProductRepository
    {
        IQueryable<Produkt> Products { get; }
        IQueryable<Matratt> Dishes { get; }
    }
}

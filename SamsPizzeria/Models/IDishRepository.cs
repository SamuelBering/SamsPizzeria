using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models
{
    public interface IDishRepository
    {
        
        IQueryable<Matratt> Dishes { get; }

        IQueryable<MatrattTyp> Categories { get; }

        IQueryable<Produkt> Products { get; }

        void AddOrUpdate(Matratt dish);

        void AddOrUpdateIngredient(Produkt produkt);
    }
}

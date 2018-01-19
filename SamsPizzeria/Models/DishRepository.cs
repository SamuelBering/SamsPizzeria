using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SamsPizzeria.Models;

namespace SamsPizzeria.Models
{


    public class DishRepository : IDishRepository

    {
        private TomasosContext _dbContext;

        public DishRepository(TomasosContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<Matratt> Dishes =>
              _dbContext.Matratt.Include(d => d.MatrattTypNavigation)
              .Include(d => d.MatrattProdukt)
                     .ThenInclude(dp => dp.Produkt);

        public void AddOrUpdate(Matratt dish)
        {

            if (dish.MatrattId != 0)
            {


                Matratt oldDish = _dbContext.Matratt.Include(d => d.MatrattProdukt)
                    .Single(d => d.MatrattId == dish.MatrattId);

                oldDish.MatrattProdukt.Clear();

                _dbContext.SaveChanges();

                oldDish.MatrattNamn = dish.MatrattNamn;
                oldDish.MatrattProdukt = dish.MatrattProdukt;
                oldDish.MatrattTyp = dish.MatrattTyp;
                oldDish.Beskrivning = dish.Beskrivning;
                oldDish.Pris = dish.Pris;

                _dbContext.AttachRange(oldDish.MatrattProdukt.Select(dp => dp.Produkt));
                _dbContext.SaveChanges();

            }
            else
            {
                _dbContext.AttachRange(dish.MatrattProdukt.Select(dp => dp.Produkt));
                _dbContext.Matratt.Add(dish);

                _dbContext.SaveChanges();

            }
        }

        public void AddOrUpdateIngredient(Produkt produkt)
        {
            _dbContext.Entry(produkt).State = produkt.ProduktId == 0 ?
                                   EntityState.Added :
                                   EntityState.Modified;

            _dbContext.SaveChanges();
        }


        public IQueryable<MatrattTyp> Categories => _dbContext.MatrattTyp;

        public IQueryable<Produkt> Products => _dbContext.Produkt;
       
    }
}

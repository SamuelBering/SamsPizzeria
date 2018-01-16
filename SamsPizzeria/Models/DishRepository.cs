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


            //_dbContext.AttachRange(dish.MatrattProdukt);


            //_dbContext.Attach(dish.MatrattTypNavigation);

            //if (dish.MatrattId == 0)
            //    _dbContext.Matratt.Add(dish);

        }

        public IQueryable<MatrattTyp> Categories => _dbContext.MatrattTyp;

        public IQueryable<Produkt> Products => _dbContext.Produkt;
       
    }
}

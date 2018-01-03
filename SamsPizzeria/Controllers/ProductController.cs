using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamsPizzeria.Models;
using SamsPizzeria.Models.ViewModels;

namespace SamsPizzeria.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult List()
        {
            var dishesDB = _repository.Dishes
                .Include(d => d.MatrattTypNavigation)
                .Include(d => d.MatrattProdukt)
                    .ThenInclude(m => m.Produkt)
                .ToList();


            List<Dish> dishesVM = dishesDB.Select(d =>
            {

                var dish = new Dish
                {
                    Id=d.MatrattId,
                    Description=d.Beskrivning,
                    Name = d.MatrattNamn,
                    Price=d.Pris,
                    Category = d.MatrattTypNavigation.Beskrivning
                };

                dish.Products = d.MatrattProdukt.Select(mp => mp.Produkt.ProduktNamn);
                return dish;
            }).ToList();


            return View(dishesVM);
        }
    }
}
using SamsPizzeria.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamsPizzeria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SamsPizzeria.Services
{
    public class DishService : IDishService
    {

        private IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public DishModificationModel GetDish(int id)
        {
            var dish = _dishRepository.Dishes.SingleOrDefault(d => d.MatrattId == id);


            if (dish != null)
            {
                var categories = _dishRepository.Categories.ToList();
                var products = _dishRepository.Products.ToList();

                DishModificationModel dishVM = CreateViewModel(dish, categories, products);

                return dishVM;
            }
            else
                return null;
        }

        private DishModificationModel CreateViewModel(Matratt d, List<MatrattTyp> categories, List<Produkt> products)
        {
            var dishVM = new DishModificationModel
            {
                Id = d.MatrattId,
                Name = d.MatrattNamn,
                Description = d.Beskrivning,
                SelectedCategoryId = d.MatrattTyp,
                SelectedCategory = d.MatrattTypNavigation.Beskrivning,
                Price = d.Pris,
                Categories = categories.Select(c =>
                    new SelectListItem
                    {
                        Value = c.MatrattTyp1.ToString(),
                        Text = c.Beskrivning,
                        Selected = d.MatrattTyp == c.MatrattTyp1
                    }).ToList(),
                Products = products.Select(p =>
                    new SelectListItem
                    {
                        Value = p.ProduktId.ToString(),
                        Text = p.ProduktNamn,
                        Selected = d.MatrattProdukt.Any(dp => dp.ProduktId == p.ProduktId)
                    }).ToList()

            };

            return dishVM;
        }

        public ICollection<DishModificationModel> GetDishes()
        {
            var categories = _dishRepository.Categories.ToList();
            var products = _dishRepository.Products.ToList();

            var dishesVM = _dishRepository.Dishes.Select(d => CreateViewModel(d, categories, products)).ToList();

            return dishesVM;
        }

    }
}

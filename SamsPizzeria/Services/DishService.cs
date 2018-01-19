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

        public DishModificationModel GetEmptyDish()
        {
            var categories = _dishRepository.Categories.ToList();
            var products = _dishRepository.Products.ToList();

            return CreateViewModel(new Matratt(), categories, products);
        }

        public DishModificationModel AddCategoriesAndProductsSelectList(DishModificationModel d)
        {
            var categories = _dishRepository.Categories.ToList();
            var products = _dishRepository.Products.ToList();

            d.Categories = categories.Select(c =>
                     new SelectListItem
                     {
                         Value = c.MatrattTyp1.ToString(),
                         Text = c.Beskrivning,
                         Selected = d.SelectedCategoryId == c.MatrattTyp1
                     }).ToList();

            d.Products = products.Select(p =>
                   new SelectListItem
                   {
                       Value = p.ProduktId.ToString(),
                       Text = p.ProduktNamn,
                       Selected = d.SelectedProductIds?.Any(id => id == p.ProduktId) ?? false
                   }).ToList();

            return d;
        }

        private DishModificationModel CreateViewModel(Matratt d, List<MatrattTyp> categories, List<Produkt> products)
        {
            var dishVM = new DishModificationModel
            {
                Id = d.MatrattId,
                Name = d.MatrattNamn,
                Description = d.Beskrivning,
                SelectedCategoryId = d.MatrattTyp,
                SelectedCategory = d.MatrattTypNavigation?.Beskrivning,
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

        public void AddOrUpdate(DishModificationModel dishVM)
        {
            Matratt dishDB = new Matratt
            {
                MatrattId = dishVM.Id,
                MatrattNamn = dishVM.Name,
                MatrattTyp = dishVM.SelectedCategoryId,
                Beskrivning = dishVM.Description,
                Pris = dishVM.Price,
                MatrattProdukt = dishVM.SelectedProductIds.Select(id =>
                  new MatrattProdukt
                  {

                      Produkt = new Produkt
                      {
                          ProduktId = id
                      }

                  }
                ).ToList()
            };
            _dishRepository.AddOrUpdate(dishDB);
        }

        public void AddOrUpdateIngredient(Ingredient ingredient)
        {
            var produkt = new Produkt
            {
                ProduktId = ingredient.Id ?? 0,
                ProduktNamn = ingredient.Name
            };

            _dishRepository.AddOrUpdateIngredient(produkt);
        }

        public ICollection<Ingredient> GetIngredients()
        {
            var ingredients = _dishRepository.Products.Select(p =>
              new Ingredient
              {
                  Id = p.ProduktId,
                  Name = p.ProduktNamn
              }).ToList();

            return ingredients;
        }

        public Ingredient GetIngredient(int id)
        {
            var product = _dishRepository.Products.SingleOrDefault(p => p.ProduktId == id);

            if (product != null)
            {
                var ingredient = new Ingredient
                {
                    Id=product.ProduktId,
                    Name=product.ProduktNamn
                };

                return ingredient;
            }
            else
                return null;
        }
    }
}

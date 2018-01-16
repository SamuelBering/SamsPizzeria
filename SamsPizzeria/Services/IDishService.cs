using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamsPizzeria.Models.ViewModels;

namespace SamsPizzeria.Services
{
    public interface IDishService
    {
        ICollection<DishModificationModel> GetDishes();

        DishModificationModel GetDish(int id);

        void AddOrUpdate(DishModificationModel dishVM);

        DishModificationModel GetEmptyDish();

        DishModificationModel AddCategoriesAndProductsSelectList(DishModificationModel d);

        void AddOrUpdateIngredient(Ingredient ingredient);

        ICollection<Ingredient> GetIngredients();

        Ingredient GetIngredient(int id);

    }
}

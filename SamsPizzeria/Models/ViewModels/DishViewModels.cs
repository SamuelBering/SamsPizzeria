using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SamsPizzeria.Models.ViewModels
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Products { get; set; }
    }

    public class DishModificationModel
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Namnet måste var minst 3 och max 40 tecken")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Pris")]
        [Required(ErrorMessage = "Pris är obligatoriskt")]
        public int Price { get; set; }
        [DisplayName("Kategori")]
        [Required(ErrorMessage = "Du måste välja en kategori")]
        public int SelectedCategoryId { get; set; }
        public string SelectedCategory { get; set; }
        public List<SelectListItem> Categories { get; set; }
        [DisplayName("Ingredienser")]
        [Required(ErrorMessage = "Du måste välja minst en ingrediens")]
        public int[] SelectedProductIds { get; set; }
        public List<SelectListItem> Products { get; set; }
    }

    public class Ingredient
    {
        [DisplayName("Ingrediensnummer")]
        public int? Id { get; set; }
        [DisplayName("Namn")]
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Namnet måste var minst 3 och max 40 tecken")]
        public string Name { get; set; }
    }
}

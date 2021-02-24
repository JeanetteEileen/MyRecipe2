using MyRecipe2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipe2.Models
{
    public class RecipeListItem
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public CuisineCategory TypeofCuisine { get; set; }
        public DishType TypeOfDish { get; set; }
    }
}

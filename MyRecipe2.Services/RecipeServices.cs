using MyRecipe2.Data;
using MyRecipe2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipe2.Services
{
    public class RecipeService
    {
        private readonly Guid _userId;
        public RecipeService(Guid userId)
        {
            _userId = userId;
        }
      
        public bool CreateRecipe(RecipeCreate model)
        {
            var entity =
                new Recipe()
                {
                    OwnerId = _userId,
                    RecipeName = model.RecipeName,
                    Instructions = model.Instructions,
                    Ingredients = model.Ingredients,
                    SourceId = model.SourceId,
                    TypeofCuisine = model.TypeofCuisine,
                    TypeOfDish = model.TypeOfDish
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Recipes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<RecipeListItem> GetRecipes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Recipes
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new RecipeListItem
                        {
                            RecipeId = e.RecipeId,
                            RecipeName = e.RecipeName,
                            TypeofCuisine = e.TypeofCuisine,
                            TypeOfDish = e.TypeOfDish
                        }
                        );
                return query.ToArray();
            }

        }
        public RecipeDetails GetRecipeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Recipes
                    .Single(e => e.RecipeId == id && e.OwnerId == _userId);
                return
                    new RecipeDetails
                    {
                        RecipeId = entity.RecipeId,
                        RecipeName = entity.RecipeName,
                        TypeofCuisine = entity.TypeofCuisine,
                        TypeOfDish = entity.TypeOfDish,
                        Ingredients = entity.Ingredients,
                        Instructions = entity.Instructions,
                        SName = entity.Source.SName,
                        SourceId = entity.Source.SourceId
                    };
            }
        }
        public IEnumerable<RecipeDetails> GetRecipesBySourceId(int id, string sName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Recipes
                    .Where(e => e.SourceId == id && e.Source.SName == sName && e.OwnerId == _userId)
                    .Select(
                            e =>
                            new RecipeDetails
                            {
                                RecipeId = e.RecipeId,
                                RecipeName = e.RecipeName,
                                TypeofCuisine = e.TypeofCuisine,
                                TypeOfDish = e.TypeOfDish,
                                Ingredients = e.Ingredients,
                                Instructions = e.Instructions,
                                SourceId = e.Source.SourceId,
                                SName = e.Source.SName
                            }
                            );
                return query.ToArray();
            }
        }
        public IEnumerable<RecipeDetails> GetRecipesSourceId(int Sourceid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Recipes
                    .Where(e => e.SourceId == Sourceid && e.OwnerId == _userId)
                    .Select(
                            e =>
                            new RecipeDetails
                            {
                                RecipeId = e.RecipeId,
                                RecipeName = e.RecipeName,
                                TypeofCuisine = e.TypeofCuisine,
                                TypeOfDish = e.TypeOfDish,
                                Ingredients = e.Ingredients,
                                Instructions = e.Instructions,
                                SourceId = e.Source.SourceId,
                                SName = e.Source.SName
                            }
                            );
                return query.ToArray();
            }
        }
        public bool UpdateRecipe(RecipeUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Recipes
                    .Single(e => e.RecipeId == model.RecipeId && e.OwnerId == _userId);

                entity.RecipeName = model.RecipeName;
                entity.TypeofCuisine = model.TypeofCuisine;
                entity.TypeOfDish = model.TypeOfDish;
                entity.Ingredients = model.Ingredients;
                entity.Instructions = model.Instructions;
                entity.SourceId = model.SourceId;

                return ctx.SaveChanges() == 1;

            }
        }
        public bool DeleteRecipe(int recipeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Recipes
                    .Single(e => e.RecipeId == recipeId && e.OwnerId == _userId);

                ctx.Recipes.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

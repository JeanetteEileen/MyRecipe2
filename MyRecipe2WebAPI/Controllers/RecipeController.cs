using MyRecipe2.Models;
using MyRecipe2.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyRecipe2.Data;

namespace MyRecipe2.WebAPI.Controllers
{
    [Authorize]
    public class RecipeController : ApiController
    {
        /// <summary>
        /// This gets all Recipes
        /// </summary>
        /// <returns>All recipes are displayed</returns>
        
        // Get all recipes api/recipe
        public IHttpActionResult Get()
        {
            RecipeService recipeServices = CreateRecipeService();
            var recipes = recipeServices.GetRecipes();
            return Ok(recipes);
        }
        /// <summary>
        /// Get a recipe by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The recipe with the given id</returns>
        
        // Get recipe by recipe ID api/recipe/id
        public IHttpActionResult Get(int id)
        {
            RecipeService recipeService = CreateRecipeService();
            var recipe = recipeService.GetRecipeById(id);
            return Ok(recipe);

        }
        /// <summary>
        /// This gets all recipes by source id and source name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sName"></param>
        /// <returns></returns>
        // Get recipes by source code (this one was more involved than it needed
        // api/recipe/sourceid=id
        public IHttpActionResult Get(int id, string sName)
        {
            RecipeService recipeService = CreateRecipeService();
            var recipes = recipeService.GetRecipesBySourceId(id, sName);
            return Ok(recipes);
        }
        /// <summary>
        /// This method is an easier way to get the recipes by SourceId
        /// </summary>
        /// <param name="Sourceid"></param>
        /// <returns>All recipes with the given SourceId</returns>
        // This is the better way api/recipe/sourceId=id
        public IHttpActionResult GetBySource(int Sourceid)
        {
            RecipeService recipeService = CreateRecipeService();
            var recipes = recipeService.GetRecipesSourceId(Sourceid);
            return Ok(recipes);

        }
        /// <summary>
        /// This creates a recipe in the database
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns>returns a 200 Ok to let you know it was completed</returns>
        public IHttpActionResult Post(RecipeCreate recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRecipeService();

            if (!service.CreateRecipe(recipe))
                return InternalServerError();

            return Ok();
        }
        private RecipeService CreateRecipeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var recipeServices = new RecipeService(userId);
            return recipeServices;
        }
        /// <summary>
        /// This updates a recipe in the database
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns>returns a 200 Ok to let you know it was completed</returns>
        public IHttpActionResult Put(RecipeUpdate recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRecipeService();

            if (!service.UpdateRecipe(recipe))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// This deletes a recipe so use with care
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns a 200 Ok to let you know it was completed</returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateRecipeService();

            if (!service.DeleteRecipe(id))
                return InternalServerError();
            return Ok();
        }
    }
}

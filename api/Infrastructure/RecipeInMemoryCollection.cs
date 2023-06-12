using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain;

namespace api.Infrastructure;

public sealed class RecipeInMemoryCollection : IRecipeRepository
{
    readonly List<Recipe> _recipes = new List<Recipe> {
		Recipe.Create(new Controllers.NewRecipeDto(){ Name = "1. Die Creme dala Creme" }),
		Recipe.Create(new Controllers.NewRecipeDto(){ Name = "2. Die Creme dala Creme" }),
		Recipe.Create(new Controllers.NewRecipeDto(){ Name = "3. Die Creme dala Creme" }),
		Recipe.Create(new Controllers.NewRecipeDto(){ Name = "4. Die Creme dala Creme" }),
	};

    public void Add(Recipe recipe)
    {
        _recipes.Add(recipe);
    }
#pragma warning disable CS1998 // not production. and doesnt cause any trouble anyways (crying about unecessary async keyword)
    public async Task<IEnumerable<Recipe>> GetAllAsync()
#pragma warning restore CS1998
    {
        return _recipes.ToList();
    }
}
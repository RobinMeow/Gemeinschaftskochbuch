using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RecipeController : GkbController
{
    readonly ILogger<RecipeController> _logger;
    readonly IRecipeRepository _recipeRepository;

    public RecipeController(
        ILogger<RecipeController> logger,
        DbContext dbContext
        )
    {
        _logger = logger;
        _recipeRepository = dbContext.RecipeRepository;
    }

    [HttpPost(nameof(Add))]
    public IActionResult Add([FromBody] NewRecipeDto newRecipe)
    {
        try
        {
            if (newRecipe.Name.Length < Recipe.NAME_MIN_LENGTH) return BadRequest(nameof(Recipe) + nameof(Recipe.Name) + " too short.");
            if (newRecipe.Name.Length > Recipe.NAME_MAX_LENGTH) return BadRequest(nameof(Recipe) + nameof(Recipe.Name) + " too long.");

            Recipe recipe = Recipe.Create(newRecipe);
            _recipeRepository.Add(recipe);

            return Ok(recipe.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CreateErrorMessage(nameof(RecipeController), nameof(Add)), newRecipe);
            return Status_500_Internal_Server_Error;
        }
    }

    [HttpGet(nameof(GetAll))]
    public async ValueTask<IActionResult> GetAll()
    {
        try
        {
            IEnumerable<Recipe> recipe = await _recipeRepository.GetAllAsync();
            return Ok(recipe.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CreateErrorMessage(nameof(RecipeController), nameof(GetAll)));
            return Status_500_Internal_Server_Error;
        }
    }
}

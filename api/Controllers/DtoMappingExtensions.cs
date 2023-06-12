using System.Collections.Generic;
using System.Linq;
using api.Domain;

namespace api.Controllers;

public static class DtoMappingExtensions
{
    public static RecipeDto ToDto(this Recipe recipe)
    {
        return new RecipeDto{
            Id = recipe.Id,
            ModelVersion = recipe.ModelVersion,
            Name = recipe.Name,
            CreatedAt = recipe.CreatedAt
        };
    }

    public static IEnumerable<RecipeDto> ToDto(this IEnumerable<Recipe> recipe)
    {
        return recipe.Select(recipe => recipe.ToDto());
    }
}

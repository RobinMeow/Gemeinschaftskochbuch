namespace api.Domain;

public abstract class DbContext
{
    public abstract IRecipeRepository RecipeRepository { get; init; }
}
using System;

namespace api.Domain;

public sealed class Recipe : Entity
{
    public const int MODEL_VERSION = 0;
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 100;

    public override int ModelVersion { get; set; } = MODEL_VERSION;

    public required string Name { get; set; }

    public static Recipe Create(api.Controllers.NewRecipeDto newRecipe) // ToDo: Make class partial and move to Application Layer (Controllers for now)
    {
        return new Recipe(){
            Id = EntityId.New(),
            CreatedAt = DateTime.UtcNow, // Make Serializer which checks for DateTimeKind and parses is to Utc if it is local
            Name = newRecipe.Name
        };
    }
}

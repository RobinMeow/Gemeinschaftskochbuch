using System;

namespace api.Domain;

public sealed class Rezept : Entity
{
    public const int MODEL_VERSION = 0;
    public const int NAME_MIN_LENGTH = 3;
    public const int NAME_MAX_LENGTH = 100;

    public override int ModelVersion { get; set; } = MODEL_VERSION;

    public required string Name { get; set; }

    public DateTime Erstelldatum { get; set; }

    public static Rezept Create(api.Controllers.NewRezeptDto newRezept) // ToDo: Make class partial and move to Application Layer (Controllers for now)
    {
        return new Rezept(){
            Id = EntityId.New(),
            Name = newRezept.Name,
            Erstelldatum = DateTime.UtcNow // Make Serializer which checks for DateTimeKind and parses is to Utc if it is local
        };
    }
}

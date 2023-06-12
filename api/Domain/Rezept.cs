using System;

namespace api.Domain;

public sealed class Rezept : Entity
{
    public static readonly int MODEL_VERSION = 0;

    public override int ModelVersion { get; set; } = MODEL_VERSION;

    public required string Name { get; set; }

    public DateTime Erstelldatum { get; set; }

    public static Rezept Create(api.Controllers.RezeptDto rezept) // ToDo: Make class partial and move to Application Layer (Controllers for now)
    {
        return new Rezept(){
            Id = EntityId.New(),
            Name = rezept.Name,
            Erstelldatum = DateTime.Now
        };
    }
}

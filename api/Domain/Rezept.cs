using System;

namespace api.Domain;

public sealed class Rezept : Entity
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public DateTime Erstelldatum { get; set; }

    public static Rezept Create(api.Controllers.RezeptDto rezept)
    {
        return new Rezept(){
            Id = Guid.NewGuid(),
            Name = rezept.Name,
            Erstelldatum = DateTime.Now
        };
    }
}

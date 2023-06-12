using System.Collections.Generic;
using System.Linq;
using api.Domain;

namespace api.Controllers;

public static class DtoMappingExtensions
{
    public static RezeptDto ToDto(this Rezept rezept)
    {
        return new RezeptDto{
            Id = rezept.Id,
            ModelVersion = rezept.ModelVersion,
            Name = rezept.Name,
            Erstelldatum = rezept.Erstelldatum
        };
    }

    public static IEnumerable<RezeptDto> ToDto(this IEnumerable<Rezept> rezepte)
    {
        return rezepte.Select(rezept => rezept.ToDto());
    }
}

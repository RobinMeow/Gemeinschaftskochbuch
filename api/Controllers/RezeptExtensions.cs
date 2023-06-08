using System.Collections.Generic;
using System.Linq;
using api.Domain;

namespace api.Controllers;

internal static class RezeptExtensions 
{
    internal static IEnumerable<RezeptDto> ToDto(this IEnumerable<Rezept> rezepte)
    {
        return rezepte.Select(rezept => new RezeptDto{
            Id = rezept.Id,
            Erstelldatum = rezept.Erstelldatum,
            Name = rezept.Name,
        });
    } 
}

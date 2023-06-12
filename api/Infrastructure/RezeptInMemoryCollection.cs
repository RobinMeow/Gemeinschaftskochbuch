using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain;

namespace api.Infrastructure;

public sealed class RezeptInMemoryCollection : IRezeptRepository
{
    readonly List<Rezept> _rezepte = new List<Rezept> {
		Rezept.Create(new Controllers.NewRezeptDto(){ Name = "1. Rezept" }),
		Rezept.Create(new Controllers.NewRezeptDto(){ Name = "2. Rezept" }),
		Rezept.Create(new Controllers.NewRezeptDto(){ Name = "3. Rezept" }),
		Rezept.Create(new Controllers.NewRezeptDto(){ Name = "4. Rezept" }),
	};

    public void Add(Rezept rezept)
    {
        _rezepte.Add(rezept);
    }
#pragma warning disable CS1998 // not production. and doesnt cause any trouble anyways (crying about unecessary async keyword)
    public async Task<IEnumerable<Rezept>> GetAllAsync()
#pragma warning restore CS1998
    {
        return _rezepte.ToList();
    }
}
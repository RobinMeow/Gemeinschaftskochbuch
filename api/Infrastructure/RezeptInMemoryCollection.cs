using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain;

namespace api.Infrastructure;

public sealed class RezeptInMemoryCollection : IRezeptRepository
{
    readonly List<Rezept> _rezepte = new List<Rezept> {
		new Rezept(){
			Id = Guid.NewGuid(),
			Erstelldatum = DateTime.Now,
			Name = "1. Rezept"
		},
		new Rezept(){
			Id = Guid.NewGuid(),
			Erstelldatum = DateTime.MaxValue,
			Name = "2. Rezept"
		},
		new Rezept(){
			Id = Guid.NewGuid(),
			Erstelldatum = DateTime.MinValue,
			Name = "3. Rezept"
		},
	};

    public void Add(Rezept rezept)
    {
        _rezepte.Add(rezept);
    }
#pragma warning disable CS1998 // not production. and doesnt cause any trouble anyways
    public async Task<IEnumerable<Rezept>> GetAll()
#pragma warning restore CS1998
    {
        return _rezepte.ToList();
    }
}
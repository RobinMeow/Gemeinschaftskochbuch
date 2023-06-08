using System;
using System.Collections.Generic;
using api.Domain;

namespace api.Infrastructure;

public sealed class RezeptRepository
{
    readonly Rezept[] _InMemoryDebugTestData = {
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
    
    internal IEnumerable<Rezept> GetAll()
    {
        return _InMemoryDebugTestData;
    }
}

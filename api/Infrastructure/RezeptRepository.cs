using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain;
using MongoDB.Driver;

namespace api.Infrastructure;

public sealed class RezeptMongoDbCollection : IRezeptRepository
{
    public const string COLLECTION_NAME = "rezepte";
	readonly IMongoCollection<Rezept> _collection;

    public RezeptMongoDbCollection(IMongoCollection<Rezept> collection)
    {
		_collection = collection;
    }

    public async void AddAsync(Rezept rezept)
    {
		await _collection.InsertOneAsync(rezept);
    }

    public async Task<IEnumerable<Rezept>> GetAllAsync()
    {
        return await _collection
			.Find<Rezept>(_ => true)
			.ToListAsync();
    }
}

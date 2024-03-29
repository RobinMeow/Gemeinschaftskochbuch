using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain;
using MongoDB.Driver;

namespace api.Infrastructure;

public sealed class ChefMongoDbCollection : IChefRepository
{
    readonly IMongoCollection<Chef> _collection;

    public ChefMongoDbCollection(IMongoDatabase database)
    {
        _collection = database.GetCollection<Chef>("chefs");
    }

    public Task AddAsync(Chef chef)
    {
        return _collection.InsertOneAsync(chef);
    }

    public async Task<IEnumerable<Chef>> GetAllAsync()
    {
        return await _collection
            .Find<Chef>(_ => true)
            .ToListAsync();
    }
}

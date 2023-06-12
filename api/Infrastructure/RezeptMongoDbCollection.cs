using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain;
using MongoDB.Driver;

namespace api.Infrastructure;

public sealed class RecipeMongoDbCollection : IRecipeRepository
{
    public const string COLLECTION_NAME = "recipes";
	readonly IMongoCollection<Recipe> _collection;

    public RecipeMongoDbCollection(IMongoCollection<Recipe> collection)
    {
		_collection = collection;
    }

    public async void Add(Recipe recipe)
    {
		await _collection.InsertOneAsync(recipe);
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync()
    {
        return await _collection
			.Find<Recipe>(_ => true)
			.ToListAsync();
    }
}

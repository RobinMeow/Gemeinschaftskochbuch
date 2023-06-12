using api.Domain;
using MongoDB.Driver;

namespace api.Infrastructure;

public sealed class MongoDbContext : DbContext
{
    public override IRezeptRepository RezeptRepository { get; init; }

    public MongoDbContext()
    : base()
    {
        string DB_NAME = Globals.ApplicationNameAbbreviated.ToLower();

        MongoClient CLIENT = new MongoClient("mongodb://localhost:27017");

        IMongoDatabase DATABASE = CLIENT.GetDatabase(DB_NAME);

        IMongoCollection<Rezept>? REZEPTE_COLLECTION = DATABASE.GetCollection<Rezept>(RezeptMongoDbCollection.COLLECTION_NAME);
        RezeptRepository = new RezeptMongoDbCollection(REZEPTE_COLLECTION);
    }
}
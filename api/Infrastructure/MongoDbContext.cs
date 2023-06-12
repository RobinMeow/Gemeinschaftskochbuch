using api.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace api.Infrastructure;

public sealed class MongoDbContext : DbContext
{
    public override IRezeptRepository RezeptRepository { get; init; }

    public MongoDbContext()
    : base()
    {
        // http://mongodb.github.io/mongo-csharp-driver/2.2/reference/bson/mapping/
        // http://mongodb.github.io/mongo-csharp-driver/2.2/reference/bson/mapping/#mapping-classes
        // https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/class-mapping/
        const string __v = "__v";
        const string _id = "_id";

        ConventionPack camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        if (!BsonClassMap.IsClassMapRegistered(typeof(Rezept)))
        {
            // Serializers (in expectation to have the same lifetime scope as ClassMaps)

            BsonClassMap.RegisterClassMap<Rezept>(x => {
                x.SetDiscriminator(nameof(Entity));
                x.AutoMap();
                // x.GetMemberMap(x => x.Erstelldatum).SetSerializer()
            });

            BsonClassMap.RegisterClassMap<Entity>(x => {
                x.AutoMap(); // EntityId
                x.MapMember(entity => entity.ModelVersion).SetElementName(__v);
                x.MapMember(x => x.Id).SetElementName(_id).SetSerializer(new EntityIdSerializer());
            });
        }

        MongoClient CLIENT = new MongoClient("mongodb://localhost:27017");
        string DB_NAME = Globals.ApplicationNameAbbreviated.ToLower();

        IMongoDatabase DATABASE = CLIENT.GetDatabase(DB_NAME);

        IMongoCollection<Rezept> REZEPTE_COLLECTION = DATABASE.GetCollection<Rezept>(RezeptMongoDbCollection.COLLECTION_NAME);


        RezeptRepository = new RezeptMongoDbCollection(REZEPTE_COLLECTION);
    }
}

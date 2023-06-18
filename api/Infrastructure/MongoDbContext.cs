using api.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace api.Infrastructure;

public sealed class MongoDbContext : DbContext
{
    public override IRecipeRepository RecipeRepository { get; init; }

    public MongoDbContext(IOptions<PersistenceSettings> persistenceSettings)
    : base()
    {
        const string __v = "__v";
        const string _id = "_id";

        ConventionPack camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        if (!BsonClassMap.IsClassMapRegistered(typeof(Recipe))) // ToDo: Check where this call belongs
        {
            // Serializers (in expectation to have the same lifetime scope as ClassMaps)
            BsonSerializer.RegisterSerializer(typeof(IsoDateTime), new BsonIsoDateTimeSerializer());

            BsonClassMap.RegisterClassMap<Entity>(x => {
                x.AutoMap(); // EntityId, CreatedAt
                x.MapMember(entity => entity.ModelVersion).SetElementName(__v);
                x.MapMember(x => x.Id).SetElementName(_id).SetSerializer(new EntityIdSerializer());
            });

            BsonClassMap.RegisterClassMap<Recipe>(x => {
                x.SetDiscriminator(nameof(Entity));
                x.AutoMap();
                // x.GetMemberMap(x => x.Erstelldatum).SetSerializer()
            });
        }

        // IDK yet if this is relevant (or how I will use DI or something else to seperate between deployment and local development)
        // string? connectionString = System.Environment.GetEnvironmentVariable("MONGODB_URI");
        // if (connectionString == null)
        // {
        //     System.Console.WriteLine("You must set your 'MONGODB_URI' environmental variable. See\n\t https://www.mongodb.com/docs/drivers/go/current/usage-examples/#environment-variable");
        //     System.Environment.Exit(0);
        // }
        MongoClientSettings settings = MongoClientSettings.FromConnectionString(persistenceSettings.Value.ConnectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        MongoClient CLIENT = new MongoClient(settings);

        string DB_NAME = Globals.ApplicationNameAbbreviated.ToLower();

        IMongoDatabase DATABASE = CLIENT.GetDatabase(DB_NAME);

        IMongoCollection<Recipe> RECIPE_COLLECTION = DATABASE.GetCollection<Recipe>(RecipeMongoDbCollection.COLLECTION_NAME);

        RecipeRepository = new RecipeMongoDbCollection(RECIPE_COLLECTION);
    }
}

// Help:
// http://mongodb.github.io/mongo-csharp-driver/2.2/reference/bson/mapping/
// http://mongodb.github.io/mongo-csharp-driver/2.2/reference/bson/mapping/#mapping-classes
// https://www.mongodb.com/docs/drivers/csharp/current/fundamentals/class-mapping/

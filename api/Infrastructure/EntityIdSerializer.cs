using System;
using api.Domain;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public sealed class EntityIdSerializer : IBsonSerializer
{
    readonly Type _valueType = typeof(EntityId); // Im scared of making it static.
    public Type ValueType { get => _valueType; }

    // Deserialize from monogodb (to C#)
    public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var reader = context.Reader;
        return new EntityId(reader.ReadString());
    }

    // Serialize to monogodb
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        string entityId = (EntityId)value;
        context.Writer.WriteString(entityId);
    }

    // use an intermediate BsonDateTime so MinValue and MaxValue are handled correctly (source: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
}

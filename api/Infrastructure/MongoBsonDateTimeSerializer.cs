using System;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public sealed class MongoBsonDateTimeSerializer : IBsonSerializer<DateTime>
{
    public Type ValueType => typeof(DateTime);

    /// <summary>Deserializes a DateTime value from BSON.</summary>
    public DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // string date = MongoDB.Bson.IO.JsonConvert.ToDateTime(context.Reader.ReadString());

        string dateTimeIsoString = context.Reader.ReadString();
        return DateTimeISO8601StringConverter.Convert(dateTimeIsoString);
    }

    /// <summary>Serializes a DateTime value to BSON.</summary>
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // WriteString(JsonConvert.ToString(value))

        string dateTimeIso = DateTimeISO8601StringConverter.Convert(value);
        context.Writer.WriteString(dateTimeIso);
    }

    /// <summary>Deserializes a DateTime value from BSON.</summary>
    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }

    /// <summary>Serializes a DateTime value to BSON.</summary>
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is DateTime dateTime)
            Serialize(context, args, dateTime);
        else
            throw new ArgumentException($"Expected value of type {typeof(DateTime)}, but got {value?.GetType()}.");
    }
}

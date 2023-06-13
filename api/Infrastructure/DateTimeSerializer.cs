using System;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public class DateTimeSerializer : IBsonSerializer<DateTime>
{
    public Type ValueType => typeof(DateTime);

    public DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // string date = MongoDB.Bson.IO.JsonConvert.ToDateTime(context.Reader.ReadString());

        string dateTimeIsoString = context.Reader.ReadString();
        return DateTime.Parse(dateTimeIsoString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // WriteString(JsonConvert.ToString(value))

        string dateTimeIso = value.ToISOString();
        context.Writer.WriteString(dateTimeIso);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    {
        if (value is DateTime dateTime)
            Serialize(context, args, dateTime);
        else
            throw new ArgumentException($"Expected value of type {typeof(DateTime)}, but got {value?.GetType()}.");
    }

    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Deserialize(context, args);
    }
}

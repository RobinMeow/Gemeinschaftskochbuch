using System;
using System.Globalization;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public class DateTimeSerializer : IBsonSerializer<DateTime>
{
    public Type ValueType => typeof(DateTime);

    static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture; // cache for performance

    static readonly string[] allowedIsoFormats = new[] {
        "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.fffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.f'Z'",
        "yyyy-MM-dd'T'HH:mm:ss'Z'"
    };

    public DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // string date = MongoDB.Bson.IO.JsonConvert.ToDateTime(context.Reader.ReadString());

        string dateTimeIsoString = context.Reader.ReadString();

        if (DateTime.TryParseExact(dateTimeIsoString, allowedIsoFormats, InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime parsedDate)) // TryParseExact is faster than Parse, if a single format is specified
            return parsedDate;
        else
            throw new ArgumentException($"Expected value of type {nameof(String)} in ISO format, but got '{dateTimeIsoString}'.");
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

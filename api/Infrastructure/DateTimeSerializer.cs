using System;
using System.Globalization;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public sealed class DateTimeSerializer : IBsonSerializer<DateTime>
{
    public Type ValueType => typeof(DateTime);

    static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture; // cache for performance

    ///<summary>Array of allowed ISO 8601 date-time formats ordered in likelyness to occur.</summary>
    static readonly string[] AllowedIsoFormats = new[] {
        "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'", // DateTime.UtcNow.ToISOString() produces 7 fractional seconds usually. It is unlikely, to call it at excat 0000000 fractional seconds (production only)
        "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.fffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.f'Z'",
        "yyyy-MM-dd'T'HH:mm:ss'Z'"
    };

    /// <summary>Deserializes a DateTime value from BSON.</summary>
    public DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // string date = MongoDB.Bson.IO.JsonConvert.ToDateTime(context.Reader.ReadString());

        string dateTimeIsoString = context.Reader.ReadString();

        if (DateTime.TryParseExact(dateTimeIsoString, AllowedIsoFormats, InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime parsedDate)) // TryParseExact should be faster than Parse, or TryParse
            return parsedDate;
        else
            throw new ArgumentException($"Expected value of type {nameof(String)} in ISO format, but got '{dateTimeIsoString}'.");
    }

    /// <summary>Serializes a DateTime value to BSON.</summary>
    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // WriteString(JsonConvert.ToString(value))

        string dateTimeIso = value.ToISOString();
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
